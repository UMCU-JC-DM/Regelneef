using Microsoft.EntityFrameworkCore;
using ClientSystem.Models;

namespace ClientSystem.Data
{
    public class ClientSystemContext : DbContext
    {
        public ClientSystemContext(DbContextOptions<ClientSystemContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DatasetRequest> DatasetRequests { get; set; }
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly define the foreign key relationship
            modelBuilder.Entity<DatasetRequest>()
                .HasOne(dr => dr.RequestedByUser)  // Navigation property
                .WithMany(u => u.DatasetRequests) // One user can have many dataset requests
                .HasForeignKey(dr => dr.RequestedBy)  // Foreign key is RequestedBy in DatasetRequest
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes
        }



    }
}