package strategypatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 8: Strategy Pattern ===\n");

        PaymentContext checkout = new PaymentContext(new CreditCardPayment("4111111111111234"));

        System.out.println("Checkout with the initial strategy (credit card):");
        checkout.executePayment(89.99);

        System.out.println("\nCustomer switches payment method at runtime to PayPal:");
        checkout.setPaymentStrategy(new PayPalPayment("hemanthi@example.com"));
        checkout.executePayment(89.99);

        System.out.println("\nSame PaymentContext object, same executePayment() call -");
        System.out.println("only the underlying strategy changed.");
    }
}
