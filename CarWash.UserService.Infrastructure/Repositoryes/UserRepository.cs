using CarWash.UserService.Domain.Entities;
using CarWash.UserService.Infrastructure.Data;
using CarWash.UserService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWash.UserService.Infrastructure.Repositoryes;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }
}