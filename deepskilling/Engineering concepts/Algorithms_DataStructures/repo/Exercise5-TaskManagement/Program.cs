namespace TaskManagement;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 5: Task Management System ===\n");

        var tasks = new TaskLinkedList();

        tasks.Add(new Task("T501", "Design database schema", "In Progress"));
        tasks.Add(new Task("T502", "Set up CI pipeline", "Pending"));
        tasks.Add(new Task("T503", "Write unit tests", "Pending"));
        tasks.Add(new Task("T504", "Deploy to staging", "Not Started"));

        Console.WriteLine($"Tasks after adding 4 (count = {tasks.Count}):");
        tasks.Traverse(t => Console.WriteLine("  " + t));

        // Search
        var found = tasks.Search("T502");
        Console.WriteLine($"\nSearch for T502: {found}");

        // Delete from the middle — no shifting required, unlike an array
        tasks.Delete("T502");
        Console.WriteLine("\nTasks after deleting T502 (no shifting — just relinking):");
        tasks.Traverse(t => Console.WriteLine("  " + t));

        // Add more — a linked list never runs out of "capacity" the way the
        // fixed array in Exercise 4 did
        tasks.Add(new Task("T505", "Write release notes", "Not Started"));
        tasks.Add(new Task("T506", "Notify stakeholders", "Not Started"));
        Console.WriteLine($"\nTasks after adding 2 more (count = {tasks.Count}):");
        tasks.Traverse(t => Console.WriteLine("  " + t));
    }
}
