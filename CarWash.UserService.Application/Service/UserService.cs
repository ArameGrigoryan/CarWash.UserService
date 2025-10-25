using CarWash.UserService.Application.IServiceInterfaces;
using CarWash.UserService.Domain.Entities;
using CarWash.UserService.Domain.Enums;
using CarWash.UserService.Infrastructure.Interfaces;

namespace CarWash.UserService.Application.Service;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task RegisterAsync(string email, string password)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Email = email,
            PasswordHash = hashedPassword,
            Role = (email == "arame@gmail.com") ? UserRole.Admin : UserRole.User
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<bool> ValidateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
    
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}