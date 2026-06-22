package adapterpatternexample;

/**
 * The interface the rest of the application programs against. Every payment
 * gateway, however its own API is shaped, gets adapted to look like this.
 */
public interface PaymentProcessor {
    void processPayment(double amount);
}
