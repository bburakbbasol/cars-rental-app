namespace CarRentalApp.WebApi.Models.Car
{
    public class UpdateCarRequest
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public decimal DailyPrice { get; set; }
        public string Color { get; set; }
    }
}
