using CarWash.UserService.Domain.Entities;

namespace CarWash.UserService.Application.IServiceInterfaces;

public interface IUserService
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task RegisterAsync(string email, string password);
    Task<bool> ValidateUserAsync(string email, string password);
    Task<List<User>> GetAllUsersAsync();
}