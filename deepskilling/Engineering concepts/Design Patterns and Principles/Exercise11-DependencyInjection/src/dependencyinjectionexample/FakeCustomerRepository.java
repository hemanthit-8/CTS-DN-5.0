package dependencyinjectionexample;

/**
 * A second, completely independent CustomerRepository implementation,
 * included specifically to demonstrate the main practical payoff of
 * Dependency Injection: because CustomerService depends on the
 * CustomerRepository INTERFACE rather than a concrete class, it can be
 * handed this lightweight, hardcoded fake instead of CustomerRepositoryImpl
 * with zero changes to CustomerService itself - exactly what a unit test
 * needs (fast, predictable, no real database involved).
 */
public class FakeCustomerRepository implements CustomerRepository {
    @Override
    public Customer findCustomerById(String id) {
        return new Customer(id, "Fake Test Customer", "fake-customer@test.local");
    }
}
