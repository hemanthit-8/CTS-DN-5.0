package decoratorpatternexample;

/**
 * Adds an SMS notification on top of whatever the wrapped Notifier already does.
 */
public class SMSNotifierDecorator extends NotifierDecorator {
    public SMSNotifierDecorator(Notifier wrappedNotifier) {
        super(wrappedNotifier);
    }

    @Override
    public void send(String message) {
        super.send(message); // let the wrapped notifier(s) send first
        System.out.println("Sending SMS notification: " + message);
    }
}
