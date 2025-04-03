using System.ComponentModel.DataAnnotations;

namespace offers.itacademy.ge.API.Models
{
    public class RegisterCompanyRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Company name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }


    }
}
