namespace TaskManagement;

/// <summary>
/// A hand-rolled singly linked list of tasks. A tail pointer is kept so that
/// appending a new task is O(1) instead of requiring a full traversal to find the end.
/// </summary>
public class TaskLinkedList
{
    private TaskNode? _head;
    private TaskNode? _tail;
    private int _count;

    public int Count => _count;

    /// <summary>
    /// Appends a task to the end of the list. O(1) thanks to the tail pointer
    /// — no traversal required, unlike a linked list without a tail reference.
    /// </summary>
    public void Add(Task task)
    {
        var node = new TaskNode(task);

        if (_head is null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            _tail!.Next = node;
            _tail = node;
        }

        _count++;
    }

    /// <summary>
    /// Searches for a task by ID by walking the chain of `Next` references
    /// from the head. O(n) — a linked list has no random access, so even the
    /// first task can only be reached by starting at the head.
    /// </summary>
    public Task? Search(string taskId)
    {
        var current = _head;
        while (current is not null)
        {
            if (current.Value.TaskId == taskId) return current.Value;
            current = current.Next;
        }
        return null;
    }

    /// <summary>Visits every task from head to tail. O(n).</summary>
    public void Traverse(Action<Task> visit)
    {
        var current = _head;
        while (current is not null)
        {
            visit(current.Value);
            current = current.Next;
        }
    }

    /// <summary>
    /// Removes the task with the given ID. O(n) overall: O(n) to find the
    /// node (and the node just before it), but only O(1) to actually unlink
    /// it once found — no shifting of other elements is needed, unlike an array.
    /// </summary>
    public bool Delete(string taskId)
    {
        TaskNode? previous = null;
        var current = _head;

        while (current is not null)
        {
            if (current.Value.TaskId == taskId)
            {
                if (previous is null)
                {
                    _head = current.Next; // removing the head
                }
                else
                {
                    previous.Next = current.Next; // unlink, O(1) once located
                }

                if (current == _tail)
                {
                    _tail = previous;
                }

                _count--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }
}
