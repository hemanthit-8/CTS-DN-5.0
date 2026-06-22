# Exercise 9: Command Pattern

## Scenario
A home automation system issues commands to turn devices on or off. The same remote control needs to work with different devices without being rewritten for each one.

## What problem the Command Pattern solves
If `RemoteControl` called `light.turnOn()` directly, it would need a hardcoded reference to every device it can control, and adding a new device type (a thermostat, a door lock) would mean modifying the remote's code to add new methods and branches. The Command Pattern wraps a request as an object: a `Command` bundles together a receiver (which device) and an action (which method to call on it) behind one uniform `execute()` method. The invoker (the remote) only ever depends on `Command`, never on `Light` or any other receiver type directly - it can trigger *any* action that's been wrapped as a `Command`, without knowing what that action actually does.

## Implementation
- `Command.java` - the command interface: `execute()`.
- `Light.java` - the receiver: a plain class that knows how to turn itself on/off, with no awareness of the Command pattern at all.
- `LightOnCommand.java`, `LightOffCommand.java` - concrete commands, each binding together a specific `Light` instance and the action to perform on it.
- `RemoteControl.java` - the invoker: holds a reference to whichever `Command` is currently assigned, and `pressButton()` triggers it - also keeps a simple execution history to show the invoker working uniformly across many different commands over time.
- `Main.java` - assigns four different commands (on/off for two different lights) to the same `RemoteControl` object in turn, demonstrating that the remote's own code never changes regardless of which light or action is behind the button.

Compile and run:
```bash
cd Exercise9-CommandPattern
javac -d out src/commandpatternexample/*.java
java -cp out commandpatternexample.Main
```

## The four roles in this pattern
- **Command** (`Command` interface) - declares the uniform action.
- **Concrete Command** (`LightOnCommand`, `LightOffCommand`) - implements the action by delegating to a specific receiver.
- **Receiver** (`Light`) - the object that actually does the work; has no knowledge of the pattern around it.
- **Invoker** (`RemoteControl`) - triggers a command without knowing what it does or who its receiver is.

This separation means the invoker, receiver, and the concrete commands binding them together can all be developed, tested, and changed independently.

## Why wrapping a request as an object is powerful
Beyond just decoupling caller from receiver, treating a request as a first-class object unlocks several things a plain method call can't easily offer: commands can be **stored** in a list (as `RemoteControl`'s `history` shows) for logging, auditing, or replay; they can be **queued** and executed later or on a different thread, useful for task scheduling; and because each command is self-contained, an `undo()` method could be added to the interface to let each concrete command reverse its own action (e.g. `LightOffCommand.undo()` calling `light.turnOn()`), which is how most real text editors implement undo/redo stacks.

## When Command might be overkill
For a single, fixed action that will never be reused, queued, logged, or undone, wrapping it in a Command class is unnecessary ceremony - a direct method call is simpler and clearer. Command earns its place when requests need to be decoupled from their execution (different parts of an application triggering the same action), stored for later, or composed into more complex operations like macros (a `MacroCommand` that holds and executes a list of other commands in sequence).
