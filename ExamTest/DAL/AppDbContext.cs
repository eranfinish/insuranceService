using DAL.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<InsurancePolicy> InsurancePolicies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InsurancePolicy>()
            .HasOne(p => p.User)
            .WithMany(u => u.InsurancePolicies)
            .HasForeignKey(p => p.UserID);
    }
}