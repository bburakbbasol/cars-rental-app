namespace CarRentalApp.Business.Models
{
    public class CarTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int PassengerCapacity { get; set; }
        public bool IsActive { get; set; }
    }
}
