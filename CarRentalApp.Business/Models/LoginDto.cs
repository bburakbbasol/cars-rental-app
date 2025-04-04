using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Business.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email alaný zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Þifre alaný zorunludur")]
        [MinLength(6, ErrorMessage = "Þifre en az 6 karakter olmalýdýr")]
        public string Password { get; set; }
    }
}
