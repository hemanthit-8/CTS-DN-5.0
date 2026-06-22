package strategypatternexample;

public class CreditCardPayment implements PaymentStrategy {
    private final String cardNumberMasked;

    public CreditCardPayment(String cardNumber) {
        // store only a masked version, as a real system would never keep a raw card number around
        this.cardNumberMasked = "**** **** **** " + cardNumber.substring(cardNumber.length() - 4);
    }

    @Override
    public void pay(double amount) {
        System.out.println("Charging $" + amount + " to credit card " + cardNumberMasked + ".");
    }
}
