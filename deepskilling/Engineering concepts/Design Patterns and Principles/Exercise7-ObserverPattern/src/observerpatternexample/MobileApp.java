package observerpatternexample;

/**
 * Concrete observer: reacts to a price update the way a mobile app would -
 * e.g. firing a push notification.
 */
public class MobileApp implements Observer {
    private final String userName;

    public MobileApp(String userName) {
        this.userName = userName;
    }

    @Override
    public void update(String stockSymbol, double newPrice) {
        System.out.println("  [MobileApp:" + userName + "] Push notification - "
                + stockSymbol + " is now $" + newPrice);
    }
}
