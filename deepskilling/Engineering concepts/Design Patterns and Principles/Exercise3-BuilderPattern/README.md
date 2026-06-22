# Exercise 3: Builder Pattern

## Scenario
A `Computer` can be assembled from many optional parts (RAM, storage, graphics card, WiFi), with only the CPU strictly required. Constructing it needs to stay readable even as the number of optional parts grows.

## What problem the Builder Pattern solves
A constructor with five-plus parameters runs into two real problems: callers must remember (or look up) the exact parameter order, and there's no good way to skip parameters they don't care about without overloading the constructor for every possible combination (a "telescoping constructor" — `Computer(cpu)`, `Computer(cpu, ram)`, `Computer(cpu, ram, storage)`, and so on, which gets unmanageable fast). The Builder Pattern separates the *construction* of a complex object from its *representation*: a separate Builder object accumulates configuration step by step through clearly named, chainable methods, and only produces the final, immutable object when `build()` is called.

## Implementation
- `Computer.java` — the immutable product class, with a `private` constructor that only `Computer.Builder` can call, plus a static nested `Builder` class. `cpu` is required and passed to the Builder's own constructor; every other field has a sensible default and an optional chained setter (`ramGb(...)`, `storageGb(...)`, `hasGraphicsCard(...)`, `hasWifi(...)`) that returns `this` for fluent chaining.
- `Main.java` — builds three different configurations: a minimal one using only defaults, a fully customized one chaining every option, and a partially customized one overriding just two fields, showing the flexibility the pattern provides.

Compile and run:
```bash
cd Exercise3-BuilderPattern
javac -d out src/builderpatternexample/*.java
java -cp out builderpatternexample.Main
```

## Why a static nested Builder class
Putting `Builder` as a `static` nested class inside `Computer` keeps the builder logically grouped with the product it builds (so `Computer.Builder` is self-documenting), while `static` means the builder doesn't need an existing `Computer` instance to be constructed — which makes sense, since its entire job is to produce one. The `Computer` constructor takes the `Builder` itself as its parameter, copying each field across; this is what allows `Computer` to remain effectively immutable (no public setters) once built, while still supporting flexible, partial configuration during the build step.

## Benefits and trade-offs
- **Benefit**: readable, self-documenting construction (`.ramGb(32).hasGraphicsCard(true)` is unambiguous in a way that a sixth positional constructor argument is not), and it naturally supports optional parameters with defaults without constructor overload explosion.
- **Benefit**: the product can enforce invariants in one place (validation logic could live in `build()`, e.g. rejecting a `Computer` with 0 GB of storage) before the immutable object is ever handed to client code.
- **Trade-off**: it's more code than a plain constructor for objects with only one or two fields — Builder earns its complexity specifically when the number of optional parameters grows large enough that a plain constructor becomes error-prone or unreadable. For simple objects, a constructor or even just public fields/records is more appropriate.
- **Related pattern note**: Builder is sometimes confused with Factory Method (Exercise 2) — the key difference is that Factory Method is about *which class* gets instantiated (polymorphic creation), while Builder is about *how a single class with many parts* gets assembled step by step.
