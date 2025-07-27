using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<SavingPackages> SavingPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.TransactionsFrom)
                .WithOne(t => t.FromUser)
                .HasForeignKey(t => t.FromAccount)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.TransactionsTo)
                .WithOne(t => t.ToUser)
                .HasForeignKey(t => t.ToAccount)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
