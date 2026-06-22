# Exercise 7: Observer Pattern

## Scenario
A stock market monitoring application needs to notify multiple clients (mobile apps, web dashboards) whenever a stock price changes, without those clients needing to constantly poll for updates.

## What problem the Observer Pattern solves
Polling - having every client repeatedly ask "has the price changed yet?" - wastes resources and introduces latency between a real change and clients noticing it. It also couples every client to knowing exactly where and how to fetch the latest price. The Observer Pattern inverts this: interested parties (observers) register themselves with the thing they care about (the subject), and the subject pushes a notification to every registered observer the moment something changes. The subject doesn't need to know what its observers are or what they do with the notification - it only depends on a shared `Observer` interface.

## Implementation
- `Observer.java` - interface with `update(stockSymbol, newPrice)`.
- `Stock.java` - subject interface with `registerObserver`, `deregisterObserver`, `notifyObservers`.
- `StockMarket.java` - concrete subject. Maintains a `List<Observer>` and a map of latest prices; `updatePrice(...)` updates state and calls `notifyObservers(...)`, which loops over every currently registered observer.
- `MobileApp.java`, `WebApp.java` - concrete observers reacting to an update in their own way (a push notification vs. refreshing a chart).
- `Main.java` - registers two `MobileApp`s and a `WebApp`, fires a price update (all three react), deregisters one `MobileApp`, then fires another update (only the remaining two react) - demonstrating that registration is dynamic and the subject's notification list reflects it immediately.

Compile and run:
```bash
cd Exercise7-ObserverPattern
javac -d out src/observerpatternexample/*.java
java -cp out observerpatternexample.Main
```

## Push vs. pull notification styles
This implementation uses a **push** style: `update(stockSymbol, newPrice)` hands the observer the data it needs directly. The alternative is a **pull** style, where `update()` takes no data and instead the observer calls back into the subject (e.g. `stockMarket.getPrice(symbol)`) to fetch what it needs. Push is simpler and avoids an extra round-trip when there's only one or two pieces of relevant data (as here); pull scales better when different observers care about different subsets of a much larger state, since each observer only fetches what it actually needs instead of receiving a large payload it mostly ignores.

## Where this pattern shows up, and a caution
Observer is the conceptual basis for event listeners in GUI frameworks, the publish/subscribe messaging pattern in distributed systems, and reactive programming libraries - any situation with a one-to-many "something changed, tell everyone who's interested" relationship. One thing to watch for in real implementations: if an observer registers itself but is never explicitly deregistered (e.g. a UI component is destroyed but forgets to unsubscribe), the subject keeps a reference to it indefinitely, which can leak memory in long-running applications. The deregistration step shown in this demo isn't just for completeness - it's necessary cleanup in production-grade observer relationships.
