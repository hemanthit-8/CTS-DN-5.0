package dependencyinjectionexample;

import java.util.HashMap;
import java.util.Map;

/**
 * Concrete repository: a real version would query a database, but the
 * point of this exercise is that CustomerService never needs to know that
 * detail - it only ever talks to the CustomerRepository interface.
 */
public class CustomerRepositoryImpl implements CustomerRepository {
    private final Map<String, Customer> customers = new HashMap<>();

    public CustomerRepositoryImpl() {
        // Seed with sample data, standing in for rows that would come from a real database.
        customers.put("C001", new Customer("C001", "Hemanthi Rao", "hemanthi@example.com"));
        customers.put("C002", new Customer("C002", "Aarav Mehta", "aarav@example.com"));
    }

    @Override
    public Customer findCustomerById(String id) {
        Customer customer = customers.get(id);
        if (customer == null) {
            System.out.println("[CustomerRepositoryImpl] No customer found for id " + id);
        }
        return customer;
    }
}
