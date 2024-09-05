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
            // Set up the one-to-many relationship between User and DatasetRequest
            modelBuilder.Entity<DatasetRequest>()
                .HasOne(dr => dr.RequestedByUser)  // Navigation property
                .WithMany(u => u.DatasetRequests) // Each User has many DatasetRequests
                .HasForeignKey(dr => dr.RequestedBy) // Foreign key in DatasetRequest
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
        }



    }
}