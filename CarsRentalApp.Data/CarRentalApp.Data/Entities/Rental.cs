using CarRentalApp.Data.Entity;

namespace CarRentalApp.Data.Entities
{
    public class Rental : BaseEntity
    {
        public string UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }

        public User User { get; set; }
        public Car Car { get; set; }
    }
}

