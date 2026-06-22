# Exercise 8: Strategy Pattern

## Scenario
A payment system needs to support multiple payment methods (Credit Card, PayPal, and others in the future), selectable at runtime, without the checkout logic needing to branch on which method was chosen.

## What problem the Strategy Pattern solves
The naive approach is an `if/else` (or `switch`) inside the checkout code: `if (method == CREDIT_CARD) {...} else if (method == PAYPAL) {...}`. Every new payment method means editing that one method again, and the checkout class accumulates knowledge of every payment method's implementation details, even though it only ever needs to know "process this payment." The Strategy Pattern extracts each algorithm (here, each way of paying) into its own class implementing a shared interface, and lets the calling code hold a reference to whichever strategy is currently active - swappable at runtime - without ever branching on type.

## Implementation
- `PaymentStrategy.java` - the strategy interface: `pay(double amount)`.
- `CreditCardPayment.java`, `PayPalPayment.java` - concrete strategies, each encapsulating its own payment details and logic (note `CreditCardPayment` stores only a masked card number, as a real system never holds a raw number after initial use).
- `PaymentContext.java` - holds a reference to the currently selected `PaymentStrategy` and delegates to it via `executePayment(amount)`; `setPaymentStrategy(...)` allows swapping the strategy at runtime.
- `Main.java` - creates a `PaymentContext` with `CreditCardPayment`, executes a payment, then swaps in `PayPalPayment` on the *same* context object and executes again, showing the context's own code never changed.

Compile and run:
```bash
cd Exercise8-StrategyPattern
javac -d out src/strategypatternexample/*.java
java -cp out strategypatternexample.Main
```

## Why this matters: open/closed principle, again
Like Factory Method (Exercise 2), Strategy follows the open/closed principle: adding `ApplePayPayment` or `CryptoPayment` later means writing one new class implementing `PaymentStrategy` - `PaymentContext` needs zero changes, and there's no `switch` statement anywhere to remember to update. This also makes each payment method independently testable in isolation, without needing to exercise the checkout flow as a whole.

## Strategy vs. State pattern, a common point of confusion
Strategy and the State pattern have nearly identical class structures (a context holding a reference to an interchangeable interface implementation), which is why they're often confused. The difference is intent: in Strategy, the *client* chooses which algorithm to plug in based on external criteria (the customer picked PayPal at checkout), and the strategy doesn't typically know about or trigger transitions to other strategies. In State, the object itself manages transitioning between states as a reaction to its own internal logic (e.g. an `OrderContext` moving itself from `PendingState` to `ShippedState` once a condition is met). If you see a pattern like this and the transitions are driven from outside, it's Strategy; if the object drives its own transitions, it's State.

## When Strategy might be overkill
For a payment method that will realistically never change (e.g. a system that only ever accepts cash), introducing an interface and a context class for a single, fixed algorithm just adds indirection without benefit. Strategy earns its place when there are genuinely multiple interchangeable algorithms now, or a near-certain need for more in the near future.
