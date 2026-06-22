package observerpatternexample;

/**
 * Subject interface: lets observers register/deregister interest, and
 * defines the notification hook the subject uses to tell them all about a change.
 */
public interface Stock {
    void registerObserver(Observer observer);
    void deregisterObserver(Observer observer);
    void notifyObservers(String stockSymbol, double newPrice);
}
