package decoratorpatternexample;

/**
 * Concrete component: the base notification channel that always exists.
 * Decorators below add channels on top of this without modifying it.
 */
public class EmailNotifier implements Notifier {
    @Override
    public void send(String message) {
        System.out.println("Sending EMAIL notification: " + message);
    }
}
