using CarRentalApp.Business.Models;

public class AddCarRequest
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string PlateNumber { get; set; }
    public decimal DailyPrice { get; set; }
    public string Color { get; set; }
    public bool IsAvailable { get; set; }
    public string?Description { get; set; } =string.Empty;
    public int CarTypeId { get; set; }


    public CarDto ToDto()
    {
        return new CarDto
        {
            Brand =this.Brand,
            Model = this.Model,
            Year = this.Year,
            PlateNumber =this.PlateNumber,
            DailyPrice = this.DailyPrice,
            Color = this.Color,
            IsAvailable = this.IsAvailable,
            Description = this.Description,
            CarTypeId = this.CarTypeId
        };
    }
}
