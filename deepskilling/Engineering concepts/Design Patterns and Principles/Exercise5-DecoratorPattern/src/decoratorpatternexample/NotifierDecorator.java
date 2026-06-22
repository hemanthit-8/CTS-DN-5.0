package decoratorpatternexample;

/**
 * Abstract decorator: implements Notifier (so it's interchangeable with any
 * other Notifier, including another decorator) and holds a reference to the
 * Notifier it wraps, delegating to it by default. Concrete decorators
 * override send() to add their own behavior around that delegation.
 */
public abstract class NotifierDecorator implements Notifier {
    protected final Notifier wrappedNotifier;

    protected NotifierDecorator(Notifier wrappedNotifier) {
        this.wrappedNotifier = wrappedNotifier;
    }

    @Override
    public void send(String message) {
        wrappedNotifier.send(message);
    }
}
