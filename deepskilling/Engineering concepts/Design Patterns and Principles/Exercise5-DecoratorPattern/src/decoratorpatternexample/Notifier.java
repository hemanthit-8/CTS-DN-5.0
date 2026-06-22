package decoratorpatternexample;

/**
 * Component interface: anything that can send a notification.
 */
public interface Notifier {
    void send(String message);
}
