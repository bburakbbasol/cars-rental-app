using CarRentalApp.Business.Models;
using CarRentalApp.Data.Entities;

namespace CarRentalApp.Business.Extensions
{
    public static class MappingExtensions
    {
        public static CarDto ToDto(this Car entity)
        {
            return new CarDto
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Year = entity.Year,
                DailyPrice = entity.DailyPrice,
                CarTypeId = entity.CarTypeId,
                Description = entity.Description,
                IsAvailable = entity.IsAvailable
            };
        }

        public static Car ToEntity(this CarDto dto)
        {
            return new Car
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                DailyPrice = dto.DailyPrice,
                CarTypeId = dto.CarTypeId,
                Description = dto.Description,
                IsAvailable = dto.IsAvailable
            };
        }

        public static void UpdateFromDto(this Car entity, CarDto dto)
        {
            entity.Brand = dto.Brand;
            entity.Model = dto.Model;
            entity.Year = dto.Year;
            entity.DailyPrice = dto.DailyPrice;
            entity.CarTypeId = dto.CarTypeId;
            entity.Description = dto.Description;
            entity.IsAvailable = dto.IsAvailable;
        }
    }
}

