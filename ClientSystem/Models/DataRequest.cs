namespace ClientSystem.Models
{
    public class DatasetRequest
    {
        public int DatasetRequestId { get; set; }  // Primary Key
        public int RequestedBy { get; set; }  // Foreign Key to User
        public string DatasetType { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";  // Status (Pending, Processing, Done)
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public User? RequestedByUser { get; set; } = null!;
    }
}