namespace ClientSystem.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }  // Primary Key
        public int UserId { get; set; }  // Foreign Key to User
        public string Action { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Details { get; set; } = string.Empty;

        public User User { get; set; } = null!;
    }
}