using System.ComponentModel.DataAnnotations;

namespace UserManagerApp.Dto
{
    public class UserValidationDto
    {
        [Required(ErrorMessage = "Username field is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password field is required.")]
        public required string Password { get; set; }
    }
}
