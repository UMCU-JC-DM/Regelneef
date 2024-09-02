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
            // Set the primary key for AuditLog
            modelBuilder.Entity<AuditLog>()
                .HasKey(al => al.LogId);

            // Configure the foreign key relationships with DeleteBehavior.Restrict
            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.Dataset)
                .WithMany()
                .HasForeignKey(al => al.DatasetId)
                .OnDelete(DeleteBehavior.Restrict);

            // Set the primary key for DistributionLog
            modelBuilder.Entity<DistributionLog>()
                .HasKey(dl => dl.DistributionId);

            // Configure the foreign key relationships with DeleteBehavior.Restrict
            modelBuilder.Entity<DistributionLog>()
                .HasOne(dl => dl.Dataset)
                .WithMany()
                .HasForeignKey(dl => dl.DatasetId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the foreign key for DistributedTo
            modelBuilder.Entity<DistributionLog>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(dl => dl.DistributedTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the foreign key for Dataset
            modelBuilder.Entity<Dataset>()
                .HasOne(d => d.CreatedByUser)
                .WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Additional configurations
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Dataset>().ToTable("Datasets");
        }
    }
}