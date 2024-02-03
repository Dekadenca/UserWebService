namespace UserManagerApp.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Language { get; set; }
        public required string Culture { get; set; }
    }
}
