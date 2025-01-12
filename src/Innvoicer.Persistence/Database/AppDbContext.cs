using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Entities.Services;
using Innvoicer.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Innvoicer.Persistence.Database;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(r => r.Email)
            .IsUnique();

        modelBuilder.Entity<Company>()
            .HasOne(c => c.User)
            .WithMany(u => u.Companies)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CompanyBankAccount>()
            .HasOne(cba => cba.Company)
            .WithMany(c => c.CompanyBankAccounts)
            .HasForeignKey(cba => cba.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}