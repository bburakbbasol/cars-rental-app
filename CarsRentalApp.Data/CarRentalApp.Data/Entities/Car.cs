using CarRentalApp.Data.Entity;

namespace CarRentalApp.Data.Entities
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<CarFeature> CarFeatures { get; set; }
        public decimal DailyPrice { get; set; }
        public int CarTypeId { get; set; }
        public string Description { get; set; }

    }
}

