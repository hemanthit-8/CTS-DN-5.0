namespace EmployeeWebApi.Models
{
    // Hands-On 3, Task 1: nested class referenced by the Employee model (List<Skill>).
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
