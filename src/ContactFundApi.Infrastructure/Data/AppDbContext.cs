using Microsoft.EntityFrameworkCore;
using ContactFundApi.Domain.Entities;

namespace ContactFundApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Fund> Funds { get; set; }
    public DbSet<ContactFund> ContactFunds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Contact entity
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        // Configure Fund entity
        modelBuilder.Entity<Fund>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Configure ContactFund junction table
        modelBuilder.Entity<ContactFund>(entity =>
        {
            entity.HasKey(e => new { e.ContactId, e.FundId });
            
            entity.HasOne(e => e.Contact)
                .WithMany(c => c.ContactFunds)
                .HasForeignKey(e => e.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Fund)
                .WithMany(f => f.ContactFunds)
                .HasForeignKey(e => e.FundId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
