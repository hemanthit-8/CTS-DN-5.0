package adapterpatternexample;

/**
 * Adapts StripeGateway's makeCharge(double dollars) to the
 * PaymentProcessor.processPayment(double amount) interface the application expects.
 */
public class StripeAdapter implements PaymentProcessor {
    private final StripeGateway stripeGateway;

    public StripeAdapter(StripeGateway stripeGateway) {
        this.stripeGateway = stripeGateway;
    }

    @Override
    public void processPayment(double amount) {
        stripeGateway.makeCharge(amount);
    }
}
