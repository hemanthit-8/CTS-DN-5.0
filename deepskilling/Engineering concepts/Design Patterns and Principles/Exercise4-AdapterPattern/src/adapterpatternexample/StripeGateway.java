package adapterpatternexample;

/**
 * A third-party gateway with its own incompatible API shape - this class
 * represents code we don't own and can't modify (the "Adaptee").
 */
public class StripeGateway {
    public void makeCharge(double amountInDollars) {
        System.out.println("Stripe: charging $" + amountInDollars + " via Stripe's API.");
    }
}
