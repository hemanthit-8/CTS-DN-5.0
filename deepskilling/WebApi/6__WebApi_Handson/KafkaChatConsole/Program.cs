using Confluent.Kafka;

// -----------------------------------------------------------------------------
// Hands-On 6, Task 1: a command-line chat app that uses Kafka as the streaming
// platform. Run it twice in two terminals:
//
//   dotnet run -- produce   (type messages, press Enter to send each one)
//   dotnet run -- consume   (prints every message as it arrives)
//
// Prerequisites (see README.md for full Kafka/Zookeeper setup):
//   Zookeeper-server-start.bat ../../config/zookeeper.properties
//   Kafka-server-start.bat ../../config/server.properties
//   kafka-topics.bat --create --topic chat-messages --bootstrap-server localhost:9092
// -----------------------------------------------------------------------------

const string bootstrapServers = "localhost:9092";
const string topic = "chat-messages";

if (args.Length == 0 || (args[0] != "produce" && args[0] != "consume"))
{
    Console.WriteLine("Usage: dotnet run -- produce|consume");
    return;
}

if (args[0] == "produce")
{
    await RunProducerAsync();
}
else
{
    RunConsumer();
}

async Task RunProducerAsync()
{
    var config = new ProducerConfig { BootstrapServers = bootstrapServers };
    using var producer = new ProducerBuilder<Null, string>(config).Build();

    Console.Write("Enter your chat name: ");
    var userName = Console.ReadLine() ?? "Anonymous";

    Console.WriteLine("Connected. Type a message and press Enter to send (Ctrl+C to quit).");

    while (true)
    {
        var message = Console.ReadLine();
        if (string.IsNullOrEmpty(message))
        {
            continue;
        }

        var payload = $"{userName}: {message}";
        var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = payload });
        Console.WriteLine($"[sent -> partition {result.Partition.Value}, offset {result.Offset.Value}]");
    }
}

void RunConsumer()
{
    var config = new ConsumerConfig
    {
        BootstrapServers = bootstrapServers,
        GroupId = "chat-console-consumers",
        AutoOffsetReset = AutoOffsetReset.Latest,
    };

    using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    consumer.Subscribe(topic);

    Console.WriteLine($"Listening on topic '{topic}'... (Ctrl+C to quit)");

    while (true)
    {
        var consumeResult = consumer.Consume();
        Console.WriteLine(consumeResult.Message.Value);
    }
}
