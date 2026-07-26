# Kafka Chat Demo (Hands-On 6)

Two chat clients that use Apache Kafka as the streaming/message-broker layer:

- `KafkaChatConsole/` — a command-line producer/consumer (Hands-On 6, Task 1).
- `KafkaChatWinForms/` — a Windows Forms chat client; run two instances to
  chat between "different client applications" (Hands-On 6, Task 2).

> Same caveat as the Web API project: no NuGet/network access in the sandbox
> this was written in, so `Confluent.Kafka` was never actually restored or
> compiled here. `KafkaChatWinForms` also only builds on Windows
> (`net8.0-windows` + WinForms). Please `dotnet restore`/`dotnet build` both
> projects yourself before pushing.

## 1. Install & start Kafka + Zookeeper locally

Download Kafka from https://kafka.apache.org/downloads, extract it, then from
the Kafka install directory:

```bash
# Terminal 1 — Zookeeper
bin/windows/zookeeper-server-start.bat config/zookeeper.properties

# Terminal 2 — Kafka broker
bin/windows/kafka-server-start.bat config/server.properties

# Terminal 3 — create the topic used by both apps below
bin/windows/kafka-topics.bat --create --topic chat-messages --bootstrap-server localhost:9092
```

(On macOS/Linux drop the `.bat` and `windows/` — use `bin/zookeeper-server-start.sh` etc.)

## 2. Console chat app

```bash
cd KafkaChatConsole
dotnet restore
dotnet run -- consume     # Terminal A — prints incoming messages
dotnet run -- produce     # Terminal B — type a name, then messages to send
```

## 3. WinForms chat app (Windows only)

Open `KafkaChatWinForms/KafkaChatWinForms.csproj` in Visual Studio on Windows,
restore, and run. Launch a **second instance** of the built .exe (or hit F5
twice) — each window is both a producer and a consumer on the same
`chat-messages` topic, so messages typed in one window appear in the other.

## Reference links (from the original hands-on)

- https://www.c-sharpcorner.com/article/apache-kafka-net-application/
- https://www.c-sharpcorner.com/article/step-by-step-installation-and-configuration-guide-of-apache-kafka-on-windows-ope/
