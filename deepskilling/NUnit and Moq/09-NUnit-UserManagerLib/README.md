# NUnit Handson 8 - UserManagerLib

Source: `8__NUnit-Handson.docx`

## What this demonstrates
- `NullReferenceException` for null/empty PAN card input
- `FormatException` for a PAN card whose length isn't 10 characters
- A happy-path test for successful user creation, alongside the exception cases

## Run
```bash
dotnet test UserManagerLib.Tests
```
