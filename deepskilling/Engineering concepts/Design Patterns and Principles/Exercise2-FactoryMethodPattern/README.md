# Exercise 2: Factory Method Pattern

## Scenario
A document management system needs to create different types of documents (Word, PDF, Excel) without the rest of the application needing to know which concrete class to instantiate.

## What problem the Factory Method Pattern solves
If client code is sprinkled with `new WordDocument()`, `new PdfDocument()`, `new ExcelDocument()` calls, adding a new document type means hunting down and editing every place objects get created, and the client code is tightly coupled to concrete classes instead of the abstraction it actually needs (`Document`). The Factory Method Pattern fixes this by defining a method whose entire job is "create the object" and letting subclasses override that one method to decide which concrete class gets produced. The rest of the class — and all client code — only ever talks to the abstract `Document` and `DocumentFactory` types.

## Implementation
- `Document.java` — interface with `open()` and `getType()`.
- `WordDocument.java`, `PdfDocument.java`, `ExcelDocument.java` — concrete implementations.
- `DocumentFactory.java` — abstract class declaring the factory method `createDocument()`, plus a shared `createAndOpenDocument()` workflow method that works identically for every document type because it only depends on the `Document` interface.
- `WordDocumentFactory.java`, `PdfDocumentFactory.java`, `ExcelDocumentFactory.java` — each overrides `createDocument()` to return its specific document type.
- `Main.java` — loops over all three factories through the shared `DocumentFactory` reference type, demonstrating that the calling code never names a concrete document class.

Compile and run:
```bash
cd Exercise2-FactoryMethodPattern
javac -d out src/factorymethodpatternexample/*.java
java -cp out factorymethodpatternexample.Main
```

## Why this matters: open/closed principle
This design follows the **open/closed principle** (the "O" in SOLID): the system is open to extension (adding a `PowerPointDocument` + `PowerPointDocumentFactory` requires zero changes to existing classes) but closed to modification (no existing `if/else` or `switch` statement needs editing to support the new type, because there isn't one — that branching logic was replaced by polymorphism). Compare this to a single `createDocument(String type)` method with a `switch` on a string/enum: every new document type would require modifying that one method, and the compiler can't catch a typo'd type string the way it catches a missing method override.

## Factory Method vs. simply calling `new`
For a fixed handful of types known at compile time that never change, a simple `switch`-based factory or even direct `new` calls might be perfectly reasonable, and using Factory Method everywhere can be over-engineering. Factory Method earns its complexity when: the set of creatable types is expected to grow, the object creation involves non-trivial setup that should be encapsulated rather than duplicated at every call site, or different parts of the application need different families of related objects (the closely related **Abstract Factory** pattern extends this idea to creating families of related objects together).
