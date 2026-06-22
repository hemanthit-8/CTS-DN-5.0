# Design Patterns & Principles — Exercise Portfolio

Eleven scenario-based exercises covering classic Gang-of-Four design patterns plus Dependency Injection, implemented in **plain Java (JDK 17+, tested on JDK 21)** with no external dependencies. Each exercise is a standalone, runnable mini-project paired with a `README.md` that works through why the pattern is needed, how the implementation maps to its standard roles, and its trade-offs.

## Exercises

| # | Exercise | Pattern category | Folder |
|---|---|---|---|
| 1 | Logging Utility | Singleton (Creational) | [`Exercise1-SingletonPattern`](Exercise1-SingletonPattern) |
| 2 | Document Management System | Factory Method (Creational) | [`Exercise2-FactoryMethodPattern`](Exercise2-FactoryMethodPattern) |
| 3 | Computer Configuration | Builder (Creational) | [`Exercise3-BuilderPattern`](Exercise3-BuilderPattern) |
| 4 | Payment Gateway Integration | Adapter (Structural) | [`Exercise4-AdapterPattern`](Exercise4-AdapterPattern) |
| 5 | Multi-Channel Notifications | Decorator (Structural) | [`Exercise5-DecoratorPattern`](Exercise5-DecoratorPattern) |
| 6 | Remote Image Viewer | Proxy (Structural) | [`Exercise6-ProxyPattern`](Exercise6-ProxyPattern) |
| 7 | Stock Market Monitoring | Observer (Behavioral) | [`Exercise7-ObserverPattern`](Exercise7-ObserverPattern) |
| 8 | Runtime-Selectable Payments | Strategy (Behavioral) | [`Exercise8-StrategyPattern`](Exercise8-StrategyPattern) |
| 9 | Home Automation | Command (Behavioral) | [`Exercise9-CommandPattern`](Exercise9-CommandPattern) |
| 10 | Student Records App | MVC (Architectural) | [`Exercise10-MVCPattern`](Exercise10-MVCPattern) |
| 11 | Customer Management | Dependency Injection (Principle) | [`Exercise11-DependencyInjection`](Exercise11-DependencyInjection) |

Each exercise's `README.md` covers: the scenario, what problem the pattern solves, how the implementation maps to the pattern's standard roles, and a trade-offs / when-to-use-it discussion — several also connect back to SOLID principles (open/closed in Exercises 2 and 8, dependency inversion in Exercises 4 and 11).

## Requirements
- JDK 17 or later (no build tool / external dependencies required — plain `javac`/`java`)

## Running an exercise
Each exercise lives in its own folder with a `src/<package>` layout. From inside any exercise folder:

```bash
javac -d out src/<package-name>/*.java
java -cp out <package-name>.Main
```

For example:
```bash
cd Exercise1-SingletonPattern
javac -d out src/singletonpatternexample/*.java
java -cp out singletonpatternexample.Main
```

The package name for each exercise matches its example-project name from the brief, lowercased (e.g. `singletonpatternexample`, `factorymethodpatternexample`, ... `dependencyinjectionexample`) — each exercise folder's own `README.md` has the exact copy-pasteable commands.

## Structure
```
Exercise1-SingletonPattern/
  src/singletonpatternexample/
    Logger.java
    Main.java
  README.md
Exercise2-FactoryMethodPattern/
  src/factorymethodpatternexample/
    ...
  README.md
...
```

Every `Main.java` is a self-contained demo: it exercises the pattern's key behavior end to end and prints what's happening to the console, so each exercise can be understood just by reading its output — no external setup, database, or network access required.
