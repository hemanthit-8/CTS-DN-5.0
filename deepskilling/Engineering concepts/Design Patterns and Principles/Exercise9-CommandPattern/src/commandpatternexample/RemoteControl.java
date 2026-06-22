package commandpatternexample;

import java.util.ArrayList;
import java.util.List;

/**
 * Invoker: holds a reference to a Command and knows how to trigger it, but
 * has no idea what the command actually does, nor what receiver (which
 * Light, in which room) is behind it. This decoupling is the whole point
 * of Command: the invoker (a remote control button) and the receiver (a
 * light) never need to know about each other directly.
 */
public class RemoteControl {
    private Command currentCommand;
    private final List<Command> history = new ArrayList<>();

    public void setCommand(Command command) {
        this.currentCommand = command;
    }

    public void pressButton() {
        if (currentCommand == null) {
            System.out.println("No command assigned to this button.");
            return;
        }
        currentCommand.execute();
        history.add(currentCommand);
    }

    public int commandsExecutedCount() {
        return history.size();
    }
}
