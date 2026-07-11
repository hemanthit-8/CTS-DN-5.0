# NUnit Handson 6 - FourSeasonsLib (TestCaseSource)

Source: `6__NUnit-Handson.docx`

## What this demonstrates
- `TestCaseSource` used two ways to avoid one test method per execution path:
  1. A static `object[]` source feeding parameters directly into the test method.
  2. A method yielding `TestCaseData` objects that drive the expected return value with
     `.Returns(...)`, where the test method itself returns the actual value for NUnit to compare.

## Run
```bash
dotnet test FourSeasonsLib.Tests
```
