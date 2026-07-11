using JwtAuthMicroserviceDemo.Models;

namespace JwtAuthMicroserviceDemo.Services
{
    public class InMemoryUserStore : IUserStore
    {
        private readonly List<UserAccount> _users = new()
        {
            new UserAccount { Username = "admin", Password = "Admin@123", Role = "Admin" },
            new UserAccount { Username = "john", Password = "John@123", Role = "User" }
        };

        public UserAccount? Validate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);
        }
    }
}
