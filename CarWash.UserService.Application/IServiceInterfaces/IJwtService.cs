namespace CarWash.UserService.Application.IServiceInterfaces;

public interface IJwtService
{
    string GenerateToken(int userId, string email, string role);
}