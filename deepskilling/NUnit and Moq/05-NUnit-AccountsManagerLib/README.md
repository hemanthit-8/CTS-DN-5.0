# NUnit Handson 4 - AccountsManagerLib

Source: `4__NUnit-Handson.docx`

## What this demonstrates
- Valid login -> `"Welcome <user_id>!!!"`
- Invalid login -> `"Invalid user id/password"`
- Missing user id or password -> exception (the shipped source throws `FormatException`;
  the doc's prose says `ArgumentException` but the actual implementation was kept as-is and
  the tests assert the real behaviour - see the note in the test file).

## Run
```bash
dotnet test AccountsManagerLib.Tests
```
