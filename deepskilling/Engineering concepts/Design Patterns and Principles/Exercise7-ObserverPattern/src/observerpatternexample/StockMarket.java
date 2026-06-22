package observerpatternexample;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Concrete subject: tracks the latest price for any number of stock symbols
 * and maintains a list of interested observers. Whenever a price changes,
 * every currently registered observer is notified - the StockMarket has no
 * idea what a MobileApp or WebApp actually does with that notification, it
 * only depends on the Observer interface.
 */
public class StockMarket implements Stock {
    private final List<Observer> observers = new ArrayList<>();
    private final Map<String, Double> latestPrices = new HashMap<>();

    @Override
    public void registerObserver(Observer observer) {
        observers.add(observer);
    }

    @Override
    public void deregisterObserver(Observer observer) {
        observers.remove(observer);
    }

    @Override
    public void notifyObservers(String stockSymbol, double newPrice) {
        for (Observer observer : observers) {
            observer.update(stockSymbol, newPrice);
        }
    }

    /** Updates the price for a symbol and notifies every registered observer. */
    public void updatePrice(String stockSymbol, double newPrice) {
        latestPrices.put(stockSymbol, newPrice);
        System.out.println("[StockMarket] " + stockSymbol + " price updated to $" + newPrice);
        notifyObservers(stockSymbol, newPrice);
    }
}
