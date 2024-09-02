namespace CentralSystem.Models
{
    public class DistributionLog
    {
        public int DistributionId { get; set; }  // Primary Key

        public int DatasetId { get; set; }  // Foreign Key to Dataset

        public int DistributedTo { get; set; }  // Foreign Key to User or Research Environment

        public DateTime DistributedAt { get; set; }

        public string Purpose { get; set; } = string.Empty;

        public Dataset Dataset { get; set; } = null!;  // Navigation Property
    }
}