using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Business.Models
{
    public class CarDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka alaný zorunludur")]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model alaný zorunludur")]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Geçerli bir yýl giriniz")]
        public int Year { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Geçerli bir fiyat giriniz")]
        public decimal DailyPrice { get; set; }

        [Required]
        public int CarTypeId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}
