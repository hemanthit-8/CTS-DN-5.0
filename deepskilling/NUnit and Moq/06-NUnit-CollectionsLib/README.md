# NUnit Handson 5 - CollectionsLib

Source: `5__NUnit-Handson.docx`

## What this demonstrates
- `CollectionAssert.AllItemsAreNotNull`, `CollectionAssert.AllItemsAreUnique`,
  `CollectionAssert.AreEquivalent`
- Classic model vs. Constraint model (`Assert.That(..., Is.EquivalentTo(...))`) assertions
- `Employee.Equals()` / `GetHashCode()` overridden (by `EmpId`) per the hand-on hint, so uniqueness
  checks are meaningful.

## Run
```bash
dotnet test CollectionsLib.Tests
```
