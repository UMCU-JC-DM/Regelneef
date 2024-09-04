namespace ClientSystem.Models
{
    public class User
    {
        public int UserId { get; set; }  // Primary Key
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;  // Data Engineer, Researcher
        public DateTime LastLogin { get; set; }
    }
}