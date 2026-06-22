# Exercise 4: Adapter Pattern

## Scenario
A payment processing system needs to integrate with multiple third-party payment gateways, each with its own incompatible API shape.

## What problem the Adapter Pattern solves
Third-party APIs are out of your control: `StripeGateway` exposes `makeCharge(double dollars)`, while `PaypalGateway` exposes a differently named `sendPayment(int cents)` that even uses a different unit. Without an adapter, checkout code would need an `if/else` (or worse, a `switch`) calling a different method with different argument conversions for every gateway, scattered across the codebase. The Adapter Pattern introduces a class that implements the interface your application *wants* (`PaymentProcessor`) and internally translates each call into whatever the wrapped third-party class actually expects — isolating the messy translation logic in exactly one place per gateway.

## Implementation
- `PaymentProcessor.java` — the target interface the rest of the application is written against: `processPayment(double amount)`.
- `StripeGateway.java`, `PaypalGateway.java` — the "adaptees": third-party-style classes with their own incompatible methods (`makeCharge(double)` vs. `sendPayment(int cents)` — note the unit mismatch too).
- `StripeAdapter.java`, `PaypalAdapter.java` — implement `PaymentProcessor` and internally delegate to the wrapped gateway, including the dollars-to-cents conversion for PayPal.
- `Main.java` — processes the same order total through both gateways via the identical `processPayment(amount)` call, proving the checkout code is gateway-agnostic.

Compile and run:
```bash
cd Exercise4-AdapterPattern
javac -d out src/adapterpatternexample/*.java
java -cp out adapterpatternexample.Main
```

## Object adapter vs. class adapter
This implementation uses **object adapter** (composition): `StripeAdapter` *holds a reference to* a `StripeGateway` and delegates to it. Java also supports a **class adapter** style via multiple interface inheritance plus extending the adaptee directly (`class StripeAdapter extends StripeGateway implements PaymentProcessor`), but that only works if the adaptee is a class you can extend (not `final`) and ties the adapter to one specific adaptee subclass. Object adapter (used here) is generally preferred in Java because composition is more flexible — the same adapter shape can wrap different implementations or even be swapped at runtime — and it avoids the fragility of inheriting from a class you don't control.

## Why this matters: isolating third-party change
If Stripe changes `makeCharge`'s signature in a future SDK version, exactly one file (`StripeAdapter.java`) needs to change — every other class in the application, including `Main`, is completely unaffected because they only depend on the stable `PaymentProcessor` interface. This is the same underlying idea as the dependency inversion principle (the "D" in SOLID, also explored in Exercise 11): high-level code (checkout logic) depends on an abstraction it controls, not on low-level details (a specific vendor's SDK) it doesn't.
