package observerpatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 7: Observer Pattern ===\n");

        StockMarket stockMarket = new StockMarket();

        Observer aliceMobile = new MobileApp("Alice");
        Observer bobMobile = new MobileApp("Bob");
        Observer webDashboard = new WebApp();

        stockMarket.registerObserver(aliceMobile);
        stockMarket.registerObserver(bobMobile);
        stockMarket.registerObserver(webDashboard);

        System.out.println("-- All three observers registered --");
        stockMarket.updatePrice("ACME", 142.50);

        System.out.println("\n-- Bob deregisters (e.g. closed the app) --");
        stockMarket.deregisterObserver(bobMobile);
        stockMarket.updatePrice("ACME", 145.10);

        System.out.println("\nNote Bob's MobileApp received the first update but not the second -");
        System.out.println("the StockMarket only ever talks to the Observer interface, so it never");
        System.out.println("needed to know or care that its observers were two MobileApps and a WebApp.");
    }
}
