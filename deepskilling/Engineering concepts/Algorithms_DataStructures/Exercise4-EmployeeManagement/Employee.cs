namespace EmployeeManagement;

/// <summary>
/// Represents an employee record.
/// </summary>
public class Employee
{
    public string EmployeeId { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }

    public Employee(string employeeId, string name, string position, decimal salary)
    {
        EmployeeId = employeeId;
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override string ToString() => $"[{EmployeeId}] {Name} - {Position} | Salary: ${Salary:N2}";
}
