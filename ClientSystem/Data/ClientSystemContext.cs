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
    }
}