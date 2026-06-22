package singletonpatternexample;

/**
 * Demonstrates that every caller across the application receives the exact
 * same Logger instance, and that they all share one consistent log history.
 */
public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 1: Singleton Pattern (Logger) ===\n");

        Logger logger1 = Logger.getInstance();
        logger1.log("Application started.");

        Logger logger2 = Logger.getInstance();
        logger2.log("Processing user request.");

        Logger logger3 = Logger.getInstance();
        logger3.log("Application shutting down.");

        System.out.println("\nlogger1 == logger2: " + (logger1 == logger2));
        System.out.println("logger2 == logger3: " + (logger2 == logger3));
        System.out.println("All three references point to the same instance: "
                + (logger1 == logger2 && logger2 == logger3));

        System.out.println("\nFull log history (recorded by the single shared instance):");
        for (String entry : logger1.getLogHistory()) {
            System.out.println("  " + entry);
        }
    }
}
