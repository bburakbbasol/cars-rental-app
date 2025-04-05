using CarRentalApp.Business.Models;
using CarRentalApp.Data.Entities;

namespace CarRentalApp.Business.Extensions
{
    public static class CarToDtoExtension
    {
        // Car entity'yi CarDto'ya dönüştürmek için extension metodu
        public static CarDto ToDto(this Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                DailyPrice = car.DailyPrice,
                Color = car.Color,
                PlateNumber = car.PlateNumber,
                IsAvailable = car.IsAvailable,
                Description = car.Description,
                CarTypeId = car.CarTypeId
            };
        }

        // CarDto'yu Car entity'sine dönüştürmek için extension metodu
        public static Car ToEntity(this CarDto carDto)
        {
            return new Car
            {
                Id = carDto.Id,
                Brand = carDto.Brand,
                Model = carDto.Model,
                Year = carDto.Year,
                DailyPrice = carDto.DailyPrice,
                Color = carDto.Color,
                PlateNumber = carDto.PlateNumber,
                IsAvailable = carDto.IsAvailable,
                Description = carDto.Description,
                CarTypeId = carDto.CarTypeId
            };
        }
    }
}
