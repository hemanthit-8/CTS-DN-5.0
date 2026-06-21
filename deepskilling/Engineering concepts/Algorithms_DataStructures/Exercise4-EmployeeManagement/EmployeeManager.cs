namespace EmployeeManagement;

/// <summary>
/// Manages employee records using a raw, fixed-capacity array (Employee[]) rather
/// than a dynamic collection like List&lt;T&gt;. This is intentional: the exercise is
/// about understanding array representation and its trade-offs, so the
/// implementation below shows real array behavior — a fixed capacity set at
/// creation time, and an O(n) shift on delete to close the resulting gap.
/// </summary>
public class EmployeeManager
{
    private readonly Employee?[] _employees;
    private int _count;

    public EmployeeManager(int capacity)
    {
        _employees = new Employee?[capacity];
        _count = 0;
    }

    public int Count => _count;
    public int Capacity => _employees.Length;

    /// <summary>
    /// Appends an employee to the end of the array. O(1) — unless the array
    /// is full, in which case there is no room to grow (a core array limitation).
    /// </summary>
    public bool Add(Employee employee)
    {
        if (_count >= _employees.Length)
        {
            Console.WriteLine($"Add failed: array is at full capacity ({_employees.Length}).");
            return false;
        }

        _employees[_count] = employee;
        _count++;
        return true;
    }

    /// <summary>
    /// Linear search by EmployeeId. O(n) — arrays have no index by key,
    /// so every search means scanning from the start until a match (or the end) is reached.
    /// </summary>
    public Employee? Search(string employeeId)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_employees[i]!.EmployeeId == employeeId)
            {
                return _employees[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Visits every employee in array order. O(n) — the minimum cost of
    /// looking at n elements, but cache-friendly because array elements are
    /// stored contiguously in memory.
    /// </summary>
    public void Traverse(Action<Employee> visit)
    {
        for (int i = 0; i < _count; i++)
        {
            visit(_employees[i]!);
        }
    }

    /// <summary>
    /// Removes the employee with the given ID and shifts every subsequent
    /// element left by one to close the gap. O(n): O(n) to find it, plus
    /// up to O(n) to shift the remaining elements.
    /// </summary>
    public bool Delete(string employeeId)
    {
        int index = -1;
        for (int i = 0; i < _count; i++)
        {
            if (_employees[i]!.EmployeeId == employeeId)
            {
                index = i;
                break;
            }
        }

        if (index == -1) return false;

        for (int i = index; i < _count - 1; i++)
        {
            _employees[i] = _employees[i + 1];
        }

        _employees[_count - 1] = null;
        _count--;
        return true;
    }
}
