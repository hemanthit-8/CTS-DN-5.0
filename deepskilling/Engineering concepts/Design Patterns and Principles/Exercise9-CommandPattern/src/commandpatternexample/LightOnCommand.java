package commandpatternexample;

/**
 * Concrete command: binds together a receiver (a specific Light) and the
 * action to invoke on it (turnOn). The command itself does no real work -
 * it just delegates to the receiver.
 */
public class LightOnCommand implements Command {
    private final Light light;

    public LightOnCommand(Light light) {
        this.light = light;
    }

    @Override
    public void execute() {
        light.turnOn();
    }
}
