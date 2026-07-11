using JwtAuthMicroserviceDemo.Models;

namespace JwtAuthMicroserviceDemo.Services
{
    public interface IUserStore
    {
        UserAccount? Validate(string username, string password);
    }
}
