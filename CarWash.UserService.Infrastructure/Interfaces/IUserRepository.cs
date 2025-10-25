using CarWash.UserService.Domain.Entities;

namespace CarWash.UserService.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task SaveChangesAsync();
    Task<List<User>> GetAllAsync();
}