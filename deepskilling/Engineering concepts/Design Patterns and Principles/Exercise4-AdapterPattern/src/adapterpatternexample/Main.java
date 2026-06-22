package adapterpatternexample;

import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 4: Adapter Pattern ===\n");

        List<PaymentProcessor> processors = List.of(
                new StripeAdapter(new StripeGateway()),
                new PaypalAdapter(new PaypalGateway())
        );

        double orderTotal = 49.99;

        System.out.println("Checking out an order for $" + orderTotal + " through each gateway,");
        System.out.println("using the SAME PaymentProcessor.processPayment(amount) call site:\n");

        for (PaymentProcessor processor : processors) {
            processor.processPayment(orderTotal);
        }

        System.out.println("\nNote: the checkout code above never knew it was talking to Stripe's");
        System.out.println("makeCharge(double) or PayPal's sendPayment(int cents) - it only called");
        System.out.println("processPayment(double), and each Adapter handled the translation.");
    }
}
