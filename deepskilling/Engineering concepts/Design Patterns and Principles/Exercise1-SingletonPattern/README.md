# Exercise 1: Singleton Pattern

## Scenario
A logging utility needs exactly one instance throughout the application lifecycle, so every part of the application writes to (and reads from) the same consistent log.

## What problem the Singleton Pattern solves
Some resources genuinely only make sense as one shared instance: a logger, a configuration manager, a connection pool, a hardware device driver. If any code could freely write `new Logger()`, different parts of the application could end up with independent, disconnected log histories — defeating the entire purpose of centralized logging. The Singleton Pattern solves this by making the class itself responsible for guaranteeing only one instance ever exists, and for providing a single, well-known way to reach it.

## Implementation
- `Logger.java` — a `private` constructor (so `new Logger()` is impossible from outside the class), a static nested `Holder` class containing the single instance, and a public `getInstance()` method.
- `Main.java` — fetches the instance three separate times through three different references and proves with `==` (reference equality) that all three point to the same object, then prints the shared log history to show every call really did land on the same instance.

Compile and run:
```bash
cd Exercise1-SingletonPattern
javac -d out src/singletonpatternexample/*.java
java -cp out singletonpatternexample.Main
```

## How thread-safety is handled
This implementation uses the **initialization-on-demand holder idiom**: the single instance lives inside a private static nested class (`Holder`). The JVM only loads and initializes a class the first time it's actively referenced, and class loading itself is guaranteed by the JVM specification to be thread-safe — so `Holder.INSTANCE` is created exactly once, lazily, with no explicit `synchronized` block or `volatile` field required.

This is an improvement over two more commonly taught (but flawed or heavier) approaches:
- **Eager initialization** (`private static final Logger INSTANCE = new Logger();` directly on `Logger`) is also thread-safe, but constructs the instance as soon as the class loads, even if it's never used — wasteful if construction is expensive.
- **Naive lazy initialization** (`if (instance == null) instance = new Logger();` inside `getInstance()`) is *not* thread-safe — two threads can both pass the null check simultaneously and create two separate instances, defeating the pattern entirely. Fixing it requires `synchronized` on every call (a performance cost) or the more intricate double-checked locking pattern with a `volatile` field.

## Trade-offs and when to be cautious with Singleton
Singleton is one of the more debated patterns in software design, for good reason:
- **Global state**: a Singleton is effectively a global variable, which can make code harder to reason about and unit test — tests can't easily substitute a fake/mock Logger, since `getInstance()` always returns the real one.
- **Hidden dependencies**: classes that call `Logger.getInstance()` internally don't expose their dependency on logging in their constructor or method signatures, making the dependency invisible to callers (this is exactly the problem Dependency Injection in Exercise 11 addresses by making dependencies explicit).
- **Concurrency within the instance**: making *construction* of the Logger thread-safe (this exercise's focus) is a different problem from making the Logger's *methods* thread-safe under concurrent calls — a production logger would also need to guard `logEntries` against concurrent modification (e.g. with a thread-safe collection), which this educational version simplifies away.

In practice, Singleton is best reserved for genuinely single-instance, mostly stateless infrastructure concerns (logging, configuration), and is often better implemented today via a dependency injection container configured to hand out a single shared instance — getting the "one instance" guarantee without the global-access drawbacks.
