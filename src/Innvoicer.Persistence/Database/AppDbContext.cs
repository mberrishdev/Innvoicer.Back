using Innvoicer.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Innvoicer.Persistence.Database;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(r => r.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}