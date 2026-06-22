package adapterpatternexample;

/**
 * Another third-party gateway, with a *different* incompatible API shape -
 * note it takes cents (an int) rather than dollars (a double), and uses a
 * completely different method name. This mismatch is exactly what the
 * Adapter Pattern exists to paper over.
 */
public class PaypalGateway {
    public void sendPayment(int amountInCents) {
        System.out.println("PayPal: sending payment of " + amountInCents + " cents via PayPal's API.");
    }
}
