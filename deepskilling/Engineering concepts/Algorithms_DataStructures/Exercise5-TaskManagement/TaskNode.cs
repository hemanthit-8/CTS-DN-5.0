namespace TaskManagement;

/// <summary>
/// A node in the singly linked list — holds a Task and a reference to the next node.
/// </summary>
public class TaskNode
{
    public Task Value { get; set; }
    public TaskNode? Next { get; set; }

    public TaskNode(Task value)
    {
        Value = value;
        Next = null;
    }
}
