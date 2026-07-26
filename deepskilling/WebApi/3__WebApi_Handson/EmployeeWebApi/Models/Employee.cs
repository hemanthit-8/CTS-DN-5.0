namespace EmployeeWebApi.Models
{
    // Hands-On 3, Task 1: the custom model class exactly as specified in the exercise.
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Salary { get; set; }
        public bool Permanent { get; set; }
        public Department? Department { get; set; }
        public List<Skill> Skills { get; set; } = new();
        public DateTime DateOfBirth { get; set; }
    }
}
