# NUnit Handson 9 - ConverterLib + Moq

Source: `9__NUnit-Handson.docx`

## What this demonstrates
- `USDToEuro` depends on an external `IDollarToEuroExchangeRateFeed` service. Moq is used to stub
  that dependency so the conversion logic can be unit tested in isolation, with a verified call
  count (`Times.Once`).
- The other pure-logic conversions (`CelsiusToKelvin`, `KilogramToPound`, `KilometerToMile`,
  `LiterToGallon`) are tested directly since they have no external dependency.

**Note:** `IDollarToEuroExchangeRateFeed` normally ships in a separate `CurrencyConverterApp.zip`
per the original hand-on instructions. That file wasn't part of the uploaded project bundle, so
the interface was recreated here (see `IDollarToEuroExchangeRateFeed.cs`) to keep the project
self-contained and buildable.

## Run
```bash
dotnet test ConverterLib.Tests
```
