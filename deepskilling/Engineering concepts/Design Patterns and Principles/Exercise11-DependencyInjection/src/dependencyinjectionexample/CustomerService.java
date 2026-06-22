package dependencyinjectionexample;

/**
 * Service: depends on CustomerRepository to do its job, but never
 * constructs one itself (no "new CustomerRepositoryImpl()" anywhere in
 * this class). The dependency is instead passed in ("injected") through
 * the constructor - this is constructor injection, the most common and
 * generally recommended form of Dependency Injection.
 */
public class CustomerService {
    private final CustomerRepository customerRepository;

    public CustomerService(CustomerRepository customerRepository) {
        this.customerRepository = customerRepository;
    }

    public void printCustomerDetails(String id) {
        Customer customer = customerRepository.findCustomerById(id);
        if (customer != null) {
            System.out.println("Found: " + customer);
        }
    }
}
