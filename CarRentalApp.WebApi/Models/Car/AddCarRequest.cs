using CarRentalApp.Business.Models;

namespace CarRentalApp.WebApi.Models.Car
{
    public class AddCarRequest
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public decimal DailyPrice { get; set; }
        public string Color { get; set; }

        public CarDto ToDto()
        {
            return new CarDto
            {
                Brand = this.Brand,
                Model = this.Model,
                Year = this.Year,
                DailyPrice = this.DailyPrice,
                
            };
        }
    }
}
