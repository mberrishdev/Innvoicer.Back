using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Entities.Clients;
using Innvoicer.Domain.Entities.Invoices;
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
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<Client> Clients { get; set; }

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

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Client)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Items)
            .WithOne()
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>()
            .HasIndex(r => r.Key)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}