# Exercise 11: Dependency Injection

## Scenario
A customer management application has a service class that depends on a repository class to fetch customer data. That dependency needs to be managed in a way that keeps the service flexible and testable.

## What problem Dependency Injection solves
If `CustomerService` constructed its own dependency internally - `this.customerRepository = new CustomerRepositoryImpl();` - it would be permanently locked to that one concrete implementation. Swapping in a different data source (a different database, a cache layer, or a fake for testing) would mean editing `CustomerService` itself. It also makes `CustomerService` impossible to unit test in isolation, since every test would transitively depend on whatever `CustomerRepositoryImpl` depends on (a real database connection, in most realistic versions of this class). Dependency Injection flips this: instead of a class constructing its own dependencies, those dependencies are handed to it ("injected") from outside - typically through the constructor - so the class only needs to know about an interface, never a concrete implementation.

## Implementation
- `Customer.java` - plain data holder.
- `CustomerRepository.java` - the interface `CustomerService` depends on: `findCustomerById(id)`.
- `CustomerRepositoryImpl.java` - a concrete implementation (here, backed by an in-memory map standing in for a real database).
- `CustomerService.java` - depends on `CustomerRepository` and receives it via **constructor injection**: the constructor takes a `CustomerRepository` parameter and stores it, with no `new CustomerRepositoryImpl()` anywhere inside the class.
- `FakeCustomerRepository.java` - a second, independent implementation that returns hardcoded data, included specifically to demonstrate DI's practical payoff for testing.
- `Main.java` - constructs `CustomerService` twice: once injected with the real repository, once injected with the fake one, with zero changes to `CustomerService.java` itself between the two.

Compile and run:
```bash
cd Exercise11-DependencyInjection
javac -d out src/dependencyinjectionexample/*.java
java -cp out dependencyinjectionexample.Main
```

## Constructor injection vs. other injection styles
- **Constructor injection** (used here): the dependency is required and passed in when the object is created. This is generally the preferred style because it makes dependencies explicit and visible in the type signature, guarantees the object is never in a partially-constructed state missing a dependency it needs, and allows the dependency field to be `final`/immutable.
- **Setter injection**: a public setter method (`setCustomerRepository(...)`) assigns the dependency after construction. Useful for optional dependencies, but it means an object can exist temporarily (or permanently, if the setter is never called) without a required dependency set.
- **Field injection**: an annotation-driven framework (like Spring's `@Autowired`) assigns the dependency directly to a field, bypassing constructors and setters entirely. Convenient, but it hides the dependency from the class's public API and makes the class harder to construct manually (e.g. in a plain unit test without the framework running).

## Why this matters: the dependency inversion principle
This exercise is a direct, hands-on illustration of the "D" in SOLID - the **dependency inversion principle**: high-level modules (`CustomerService`) should not depend on low-level modules (`CustomerRepositoryImpl`) directly; both should depend on abstractions (`CustomerRepository`). The Adapter Pattern (Exercise 4) and Strategy Pattern (Exercise 8) both lean on this same idea in different contexts - depending on an interface rather than a concrete class is what makes a system's pieces independently replaceable.

## DI containers, briefly
Manually constructing and wiring dependencies (as `Main.java` does here) is called "poor man's DI" - perfectly fine for small programs, but it doesn't scale well to applications with deep dependency graphs (a service that needs three repositories, each of which needs its own dependencies, and so on). Frameworks like Spring (Java) or .NET's built-in DI container automate this wiring: you register which concrete implementation should satisfy each interface once, and the framework constructs and injects the entire object graph automatically. The underlying principle demonstrated in this exercise - depend on abstractions, inject concrete implementations from outside - is exactly what those frameworks are built to automate at scale.
