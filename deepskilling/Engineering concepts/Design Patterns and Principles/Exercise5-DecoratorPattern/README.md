# Exercise 5: Decorator Pattern

## Scenario
A notification system needs to send notifications through multiple channels (Email, SMS, Slack), and which channels are active for a given notification needs to be configurable, ideally without an explosion of subclasses for every combination.

## What problem the Decorator Pattern solves
If every channel combination needed its own subclass (`EmailNotifier`, `EmailAndSmsNotifier`, `EmailAndSlackNotifier`, `EmailAndSmsAndSlackNotifier`...), the number of classes would grow combinatorially with the number of channels - 3 channels means up to 7 meaningful combinations, and a 4th channel would push that past 15. The Decorator Pattern avoids this by letting behavior be added at runtime through composition: each decorator wraps a `Notifier` and adds its own behavior around delegating to the wrapped one, so any combination of channels is just a matter of how many decorators you stack, not how many classes you've defined.

## Implementation
- `Notifier.java` - the component interface, with `send(String message)`.
- `EmailNotifier.java` - the concrete (base) component.
- `NotifierDecorator.java` - an abstract decorator that itself implements `Notifier` and holds a reference to the wrapped `Notifier`, delegating to it by default.
- `SMSNotifierDecorator.java`, `SlackNotifierDecorator.java` - concrete decorators that call `super.send(message)` (triggering whatever is wrapped underneath) and then add their own channel's notification.
- `Main.java` - sends the same message through three configurations: email-only, email+SMS, and email+SMS+Slack, all stacked from the same building blocks at runtime.

Compile and run:
```bash
cd Exercise5-DecoratorPattern
javac -d out src/decoratorpatternexample/*.java
java -cp out decoratorpatternexample.Main
```

## How the wrapping/delegation works
`new SlackNotifierDecorator(new SMSNotifierDecorator(new EmailNotifier()))` builds a chain: the outermost `SlackNotifierDecorator` holds a reference to the `SMSNotifierDecorator`, which holds a reference to the `EmailNotifier`. Calling `send()` on the outermost object calls `super.send()` first (delegating down the chain to email, then SMS), and only after that returns does it run its own Slack-specific logic - so the order of construction directly controls the order notifications fire in. Because `NotifierDecorator` itself implements `Notifier`, decorators can wrap other decorators arbitrarily deep, and any caller holding a `Notifier` reference doesn't need to know how many layers are underneath.

## Decorator vs. subclassing, and when to reach for it
Subclassing (inheritance) fixes behavior at compile time and applies it to every instance of that subclass; Decorator adds behavior at runtime, per-instance, and lets behaviors be combined freely in any order or count. Decorator is the right tool when: behavior needs to be added/removed dynamically (e.g. a user toggling SMS notifications on and off in their settings), the number of optional behaviors would otherwise produce a combinatorial explosion of subclasses, or you want to keep the option to combine behaviors in ways that weren't anticipated when the base classes were written. It's overkill for one or two genuinely fixed variations, where a couple of straightforward subclasses are simpler to read and debug (the indirection of unwrapping a deeply nested decorator chain can make stack traces harder to follow).
