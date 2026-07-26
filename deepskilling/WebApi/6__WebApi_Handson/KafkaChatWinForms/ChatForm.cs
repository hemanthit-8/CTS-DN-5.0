using Confluent.Kafka;

namespace KafkaChatWinForms;

/// <summary>
/// Hands-On 6, Task 2: a minimal WinForms chat client. Each running instance
/// is both a producer (sends what you type) and a consumer (shows messages
/// from every other running instance) on the same Kafka topic, so launching
/// this app twice demonstrates "different client applications" chatting
/// through Kafka.
/// </summary>
public partial class ChatForm : Form
{
    private const string BootstrapServers = "localhost:9092";
    private const string Topic = "chat-messages";

    private readonly TextBox _messageBox = new() { Dock = DockStyle.Bottom, Height = 30 };
    private readonly Button _sendButton = new() { Text = "Send", Dock = DockStyle.Bottom, Height = 30 };
    private readonly ListBox _chatLog = new() { Dock = DockStyle.Fill };
    private readonly string _userName;

    private IProducer<Null, string>? _producer;
    private CancellationTokenSource? _consumerCts;

    public ChatForm()
    {
        Text = "Kafka Chat";
        Width = 500;
        Height = 500;

        Controls.Add(_chatLog);
        Controls.Add(_messageBox);
        Controls.Add(_sendButton);

        _userName = $"User-{Environment.ProcessId}";
        _sendButton.Click += SendButton_Click;

        Load += ChatForm_Load;
        FormClosing += ChatForm_FormClosing;
    }

    private void ChatForm_Load(object? sender, EventArgs e)
    {
        _producer = new ProducerBuilder<Null, string>(new ProducerConfig { BootstrapServers = BootstrapServers }).Build();

        _consumerCts = new CancellationTokenSource();
        Task.Run(() => ConsumeLoop(_consumerCts.Token));

        _chatLog.Items.Add($"Connected as {_userName}. Messages from every running instance will appear here.");
    }

    private async void SendButton_Click(object? sender, EventArgs e)
    {
        var text = _messageBox.Text;
        if (string.IsNullOrWhiteSpace(text) || _producer == null)
        {
            return;
        }

        await _producer.ProduceAsync(Topic, new Message<Null, string> { Value = $"{_userName}: {text}" });
        _messageBox.Clear();
    }

    private void ConsumeLoop(CancellationToken token)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = $"winforms-chat-{Guid.NewGuid()}", // unique group so every window sees every message
            AutoOffsetReset = AutoOffsetReset.Latest,
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(Topic);

        try
        {
            while (!token.IsCancellationRequested)
            {
                var result = consumer.Consume(token);
                if (result?.Message != null)
                {
                    Invoke(() => _chatLog.Items.Add(result.Message.Value));
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Expected on form close.
        }
    }

    private void ChatForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        _consumerCts?.Cancel();
        _producer?.Flush(TimeSpan.FromSeconds(2));
        _producer?.Dispose();
    }
}
