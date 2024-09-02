namespace CentralSystem.Models
{
    public class Dataset
    {
        public int DatasetId { get; set; }  // Primary Key

        public int CreatedBy { get; set; }  // Foreign Key to User

        public DateTime CreatedAt { get; set; }

        public string SoftwareVersion { get; set; } = string.Empty;

        public string DataSource { get; set; } = string.Empty;  // Reference to preprocessed data model

        public string Purpose { get; set; } = string.Empty;

        public User CreatedByUser { get; set; } = null!;  // Navigation Property
    }
}
