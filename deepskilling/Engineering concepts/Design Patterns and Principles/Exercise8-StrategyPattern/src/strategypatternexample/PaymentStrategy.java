package strategypatternexample;

/**
 * Strategy interface: a family of interchangeable algorithms for "how to pay",
 * all exposed through the same method signature.
 */
public interface PaymentStrategy {
    void pay(double amount);
}
