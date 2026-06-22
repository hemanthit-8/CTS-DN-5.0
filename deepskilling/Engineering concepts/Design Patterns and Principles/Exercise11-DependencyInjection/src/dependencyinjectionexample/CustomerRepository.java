package dependencyinjectionexample;

/**
 * Repository interface: the abstraction CustomerService depends on. It
 * says nothing about HOW a customer is found (a database, an in-memory
 * map, a remote API) - that's left entirely to implementations.
 */
public interface CustomerRepository {
    Customer findCustomerById(String id);
}
