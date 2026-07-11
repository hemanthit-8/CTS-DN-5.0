namespace JwtAuthMicroserviceDemo.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }
}
