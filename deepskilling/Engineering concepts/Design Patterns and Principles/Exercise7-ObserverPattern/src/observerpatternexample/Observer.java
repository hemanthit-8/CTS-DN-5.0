package observerpatternexample;

/**
 * Anything that wants to be notified when a stock price changes implements this.
 */
public interface Observer {
    void update(String stockSymbol, double newPrice);
}
