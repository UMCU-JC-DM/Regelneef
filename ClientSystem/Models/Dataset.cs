namespace ClientSystem.Models
{
    public class Dataset
    {
        public int DatasetId { get; set; }  // Primary Key
        public int DatasetRequestId { get; set; }  // Foreign Key to DatasetRequest
        public string DataSource { get; set; } = string.Empty;
        public string Status { get; set; } = "Generated";  // Generated, Delivered, etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DatasetRequest DatasetRequest { get; set; } = null!;
    }
}