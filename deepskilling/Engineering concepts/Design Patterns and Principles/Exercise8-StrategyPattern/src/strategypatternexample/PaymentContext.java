package strategypatternexample;

/**
 * Context: holds a reference to whichever PaymentStrategy is currently
 * selected, and delegates to it. The context's own code never changes
 * regardless of which concrete strategy is plugged in, and the strategy
 * can be swapped at runtime via setPaymentStrategy().
 */
public class PaymentContext {
    private PaymentStrategy paymentStrategy;

    public PaymentContext(PaymentStrategy paymentStrategy) {
        this.paymentStrategy = paymentStrategy;
    }

    public void setPaymentStrategy(PaymentStrategy paymentStrategy) {
        this.paymentStrategy = paymentStrategy;
    }

    public void executePayment(double amount) {
        paymentStrategy.pay(amount);
    }
}
