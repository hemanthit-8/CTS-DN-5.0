namespace EmployeeManagement;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 4: Employee Management System ===\n");

        var manager = new EmployeeManager(capacity: 5);

        manager.Add(new Employee("E401", "Kavya Reddy", "Software Engineer", 85000m));
        manager.Add(new Employee("E402", "Arjun Nair", "Product Manager", 95000m));
        manager.Add(new Employee("E403", "Divya Pillai", "QA Engineer", 70000m));
        manager.Add(new Employee("E404", "Karthik Menon", "DevOps Engineer", 88000m));

        Console.WriteLine($"Employees after adding 4 (capacity = {manager.Capacity}):");
        manager.Traverse(e => Console.WriteLine("  " + e));

        // Search
        var found = manager.Search("E402");
        Console.WriteLine($"\nSearch for E402: {found}");

        var notFound = manager.Search("E999");
        Console.WriteLine($"Search for E999: {(notFound is null ? "not found" : notFound.ToString())}");

        // Delete from the middle, to show the shift
        manager.Delete("E402");
        Console.WriteLine("\nEmployees after deleting E402 (note the shift to fill the gap):");
        manager.Traverse(e => Console.WriteLine("  " + e));

        // Demonstrate the fixed-capacity limitation
        manager.Add(new Employee("E405", "Meera Iyer", "HR Specialist", 65000m));
        manager.Add(new Employee("E406", "Rahul Verma", "Data Analyst", 72000m));
        Console.WriteLine($"\nEmployees after adding 2 more (now at {manager.Count}/{manager.Capacity}):");
        manager.Traverse(e => Console.WriteLine("  " + e));

        Console.WriteLine("\nAttempting to add one more employee beyond capacity:");
        manager.Add(new Employee("E407", "Sanjay Kumar", "Intern", 30000m));
    }
}
