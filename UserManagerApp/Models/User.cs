namespace UserManagerApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Language { get; set; }
        public required string Culture { get; set; }
        public required string Password { get; set; }

    }
}
