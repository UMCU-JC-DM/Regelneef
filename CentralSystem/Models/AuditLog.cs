namespace CentralSystem.Models
{
    public class AuditLog
    {
        public int LogId { get; set; }  // Primary Key

        public int UserId { get; set; }  // Foreign Key to User

        public int DatasetId { get; set; }  // Foreign Key to Dataset

        public string Action { get; set; } = string.Empty;  // e.g., "created", "accessed", "modified", "analyzed"

        public DateTime Timestamp { get; set; }

        public string Details { get; set; } = string.Empty;

        public User User { get; set; } = null!;  // Navigation Property

        public Dataset Dataset { get; set; } = null!;  // Navigation Property
    }
}
