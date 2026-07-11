# NUnit Handson 2 - Calculator (Parameterised tests & void methods)

Source: `2__NUnit-Handson.docx`

## What this demonstrates
- Parameterised test cases for Subtraction / Multiplication / Division (`TestCase`, `Assert.AreEqual`)
- Divide-by-zero handled with try/catch around `ArgumentException` and `Assert.Fail("Division by zero")`
- Testing a void method (`AllClear`) via the `GetResult` property (`TestAddAndClear`)

## Run
```bash
dotnet test CalcLibrary.Tests
```
