using System.ComponentModel.DataAnnotations;

namespace UserManagerApp.Dto
{
    public class UserPostDto
    {
        //Max length is determined by size of the field in database.
        [Required, StringLength(255)]
        public required string UserName { get; set; }

        //Max length is determined by size of the field in database.
        [Required, StringLength(255)]
        public required string FullName { get; set; }

        //Standard email format
        [Required, EmailAddress, StringLength(255)]
        public required string Email { get; set; }

        [StringLength(63)]
        public string? MobileNumber { get; set; }

        //Language is in alpha-2 code format
        [StringLength(2)]
        public string? Language { get; set; }

        [Required, StringLength(63)]
        public required string Culture { get; set; }

        //Minimum eight characters, at least one letter, one number and one special character
        [
            Required,
            RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$", ErrorMessage = "Password must have minimum eight characters, at least one letter, one number and one special character"),
            StringLength(255)
        ]
        public required string Password { get; set; }

    }
}
