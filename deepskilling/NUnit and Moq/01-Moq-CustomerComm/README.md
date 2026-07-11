# Moq Handson - CustomerCommLib

Source: `1__Moq-Handson.docx`

## What this demonstrates
- Mocking, isolation, and test doubles (Mock vs Fake vs Stub)
- Dependency Injection (constructor injection) to make code testable
- Writing testable code with Moq so a unit test never triggers a real SMTP send

## Projects
- **CustomerCommLib** - `IMailSender` / `MailSender` (talks to an SMTP server) and `CustomerComm`
  (the class under test), which receives `IMailSender` through its constructor.
- **CustomerCommLib.Tests** - NUnit + Moq tests. `IMailSender` is mocked so `SendMail()` accepts
  any two strings and always returns `true`, proving `CustomerComm` works correctly without ever
  touching a real mail server.

## Run
```bash
dotnet test CustomerCommLib.Tests
```
