using CarWash.UserService.Domain.Entities;
using CarWash.UserService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CarWash.UserService.Infrastructure.Data;
public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);

    }
}