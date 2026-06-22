package dependencyinjectionexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 11: Dependency Injection ===\n");

        System.out.println("-- Production setup: real repository injected --");
        CustomerService realService = new CustomerService(new CustomerRepositoryImpl());
        realService.printCustomerDetails("C001");
        realService.printCustomerDetails("C999"); // not found, exercises that path too

        System.out.println("\n-- Test-style setup: fake repository injected instead --");
        CustomerService testService = new CustomerService(new FakeCustomerRepository());
        testService.printCustomerDetails("ANY-ID");

        System.out.println("\nCustomerService.java was never recompiled or edited between these two");
        System.out.println("runs - only WHICH CustomerRepository implementation was handed to its");
        System.out.println("constructor changed. That swap is the entire point of Dependency Injection.");
    }
}
