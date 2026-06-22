package observerpatternexample;

/**
 * Concrete observer: reacts to a price update the way a web dashboard
 * would - e.g. refreshing a chart on screen.
 */
public class WebApp implements Observer {
    @Override
    public void update(String stockSymbol, double newPrice) {
        System.out.println("  [WebApp] Refreshing dashboard chart - "
                + stockSymbol + " is now $" + newPrice);
    }
}
