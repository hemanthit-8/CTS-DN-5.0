package decoratorpatternexample;

/**
 * Adds a Slack notification on top of whatever the wrapped Notifier already does.
 */
public class SlackNotifierDecorator extends NotifierDecorator {
    public SlackNotifierDecorator(Notifier wrappedNotifier) {
        super(wrappedNotifier);
    }

    @Override
    public void send(String message) {
        super.send(message);
        System.out.println("Sending SLACK notification: " + message);
    }
}
