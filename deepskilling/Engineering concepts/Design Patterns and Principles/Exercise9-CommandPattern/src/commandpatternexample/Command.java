package commandpatternexample;

/**
 * Command interface: wraps a request (and everything needed to carry it
 * out) as an object, so it can be passed around, queued, or invoked later
 * without the invoker knowing what the command actually does.
 */
public interface Command {
    void execute();
}
