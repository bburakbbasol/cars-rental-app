using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Business.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email alan� zorunludur")]
        [EmailAddress(ErrorMessage = "Ge�erli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "�ifre alan� zorunludur")]
        [MinLength(4, ErrorMessage = "�ifre en az 4 karakter olmal�d�r")]
        public string Password { get; set; }
    }
}
