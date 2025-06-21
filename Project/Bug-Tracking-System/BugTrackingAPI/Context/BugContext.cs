using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Context
{
    public class BugContext : DbContext
    {
        public BugContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Bugs> Bugs { get; set; }

        public DbSet<BugAssignment> BugAssignments { get; set; }

        public DbSet<BugComment> BugComments { get; set; }
        
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

        public DbSet<BlackListedToken> BlackListedTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BugAssignment → Developer (Employee)
            modelBuilder.Entity<BugAssignment>()
                .HasOne(ba => ba.Developer)
                .WithMany(e => e.AssignedBugs)
                .HasForeignKey(ba => ba.DevId)
                .OnDelete(DeleteBehavior.Restrict);

            // BugAssignment → Admin (Employee)
            modelBuilder.Entity<BugAssignment>()
                .HasOne(ba => ba.Admin)
                .WithMany(e => e.AssignedByAdmin)
                .HasForeignKey(ba => ba.AssignedById)
                .OnDelete(DeleteBehavior.Restrict);


        }


    }
}