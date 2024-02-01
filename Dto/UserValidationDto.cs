namespace UserManagerApp.Dto
{
    public class UserValidationDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}
