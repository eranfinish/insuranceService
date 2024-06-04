using ExamTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamTest.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.InsurancePolicies)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
