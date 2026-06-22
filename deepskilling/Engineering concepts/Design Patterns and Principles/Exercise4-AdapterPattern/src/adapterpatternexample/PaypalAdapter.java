package adapterpatternexample;

/**
 * Adapts PaypalGateway's sendPayment(int cents) to the
 * PaymentProcessor.processPayment(double amount) interface, including the
 * dollars-to-cents unit conversion the two APIs disagree on.
 */
public class PaypalAdapter implements PaymentProcessor {
    private final PaypalGateway paypalGateway;

    public PaypalAdapter(PaypalGateway paypalGateway) {
        this.paypalGateway = paypalGateway;
    }

    @Override
    public void processPayment(double amount) {
        int amountInCents = (int) Math.round(amount * 100);
        paypalGateway.sendPayment(amountInCents);
    }
}
