# .NET Unit Testing & Mocking Hands-On Labs (NUnit + Moq)

This repository contains 10 self-contained, buildable .NET 8 solutions, each converted from a
legacy .NET Framework hands-on exercise into a modern SDK-style project with the tasks from its
corresponding hand-on document already implemented.

Every folder is independent: its own class-library source project, its own NUnit (and, where
required, Moq) test project, its own `.sln`, and its own `README.md` explaining what the doc asked
for and what was built.

| # | Folder | Source hand-on doc | Focus |
|---|--------|--------------------|-------|
| 01 | `01-Moq-CustomerComm` | `1__Moq-Handson.docx` | Mocking, DI, testable code with Moq |
| 02 | `02-NUnit-Calculator-Basics` | `1__NUnit-Handson.docx` | `TestFixture`, `SetUp`/`TearDown`, `TestCase` |
| 03 | `03-NUnit-Calculator-Parameterized` | `2__NUnit-Handson.docx` | Parameterised tests, exceptions, void methods |
| 04 | `04-NUnit-UtilLib-HostNameParser` | `3__NUnit-Handson.docx` | Custom attributes, multiple execution paths |
| 05 | `05-NUnit-AccountsManagerLib` | `4__NUnit-Handson.docx` | Return-value & exception testing |
| 06 | `06-NUnit-CollectionsLib` | `5__NUnit-Handson.docx` | `CollectionAssert`, classic vs constraint model |
| 07 | `07-NUnit-FourSeasonsLib-TestCaseSource` | `6__NUnit-Handson.docx` | `TestCaseSource` (two styles) |
| 08 | `08-NUnit-LeapYearCalculatorLib` | `7__NUnit-Handson.docx` | Parameterised + boundary-value testing |
| 09 | `09-NUnit-UserManagerLib` | `8__NUnit-Handson.docx` | Exception-focused testing |
| 10 | `10-NUnit-Moq-ConverterLib` | `9__NUnit-Handson.docx` | Moq for an external-service dependency |

## Stack

- **.NET 8.0** (SDK-style `csproj`, cross-platform - builds on Windows/macOS/Linux and in CI)
- **NUnit 4** + **NUnit3TestAdapter**
- **Moq** (projects 01 and 10)
- Naming conventions follow each doc's recommendations: `<ClassLib>.Tests` project name,
  `<SUT>Tests` class name, `UnitUnderTest_Scenario_ExpectedOutcome` method name, and
  `Assert.That()` (constraint model) used throughout except where a doc explicitly asked for
  classic-model asserts.

## Getting started

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
# From inside any numbered folder:
cd 01-Moq-CustomerComm
dotnet test

# Or, to build/test every project from the repo root:
find . -name "*.Tests.csproj" -exec dotnet test {} \;
```

## Deploying to your own GitHub repo

```bash
git init
git add .
git commit -m "Add NUnit/Moq hands-on lab solutions"
git branch -M main
git remote add origin <your-repo-url>
git push -u origin main
```

A ready-to-use GitHub Actions workflow (`.github/workflows/dotnet-tests.yml`) is included and will
build and run every project's tests automatically on each push/PR.

## Notes on faithfulness to the original source

- All source class-library code (`AccountsManagerLib`, `CalcLibrary`, `CollectionsLib`,
  `ConverterLib`, `FourSeasonsLib`/`SeasonsLib`, `LeapYearCalculatorLib`, `UserManagerLib`,
  `UtilLib`) was carried over from the supplied `Parent_project_code.zip`, only modernised to
  SDK-style `csproj` targeting `net8.0` (no behavioural changes).
- `10-NUnit-Moq-ConverterLib` recreates the `IDollarToEuroExchangeRateFeed` interface, which the
  original hand-on sources from a separate `CurrencyConverterApp.zip` that wasn't included in the
  uploaded bundle - see that project's `README.md` for details.
- `01-Moq-CustomerComm`'s `CustomerCommLib` is newly written per the doc's Task 1/Task 2
  instructions (it wasn't part of `Parent_project_code.zip`).
- Each per-doc test file calls out, in code comments, any place where the doc's prose and the
  actual shipped source code disagree (e.g. `05-NUnit-AccountsManagerLib`), so tests always assert
  the real, observable behaviour of the code rather than the written description.

## Verification

This environment doesn't have network access to nuget.org / the .NET SDK, so the projects could
not be `dotnet build`/`dotnet test`-verified here. The code was written and reviewed carefully for
correctness, but please run `dotnet test` locally (or let the included CI workflow do it) before
relying on it.
