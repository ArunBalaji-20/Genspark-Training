using BankingAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPP.Contexts
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<User> Users  { get; set; }
        public DbSet<Transaction> Transactions  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.AccountNumber)
                                    .HasName("PK_User_AccountNumber");

            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId)
                                            .HasName("PK_Transaction_TransactionId");

            modelBuilder.Entity<Transaction>()
                        .HasOne(t => t.User)
                        .WithMany(u => u.transactions)
                        .HasForeignKey(t => t.AccountNumber)
                        .HasConstraintName("FK_Transaction_User_AccountNumber")
                        .OnDelete(DeleteBehavior.Restrict);
        }

    }
}