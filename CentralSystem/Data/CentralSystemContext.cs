using Microsoft.EntityFrameworkCore;
using CentralSystem.Models;

namespace CentralSystem.Data
{
    public class CentralSystemContext : DbContext
    {
        public CentralSystemContext(DbContextOptions<CentralSystemContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<DistributionLog> DistributionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Additional configurations (optional)
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Dataset>().ToTable("Datasets");
            modelBuilder.Entity<AuditLog>().ToTable("AuditLogs");
            modelBuilder.Entity<DistributionLog>().ToTable("DistributionLogs");
        }
    }
}
