using CarRentalApp.Business.Extensions;
using CarRentalApp.Business.Models;
using CarRentalApp.Data.Entities;
using CarRentalApp.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApp.Business.Operations
{
    public class CarManager : ICarManager
    {
        private readonly IRepository<Car> _carRepository;

        public CarManager(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        // Get all cars
        public async Task<OperationResult<IEnumerable<CarDto>>> GetAllAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            var carDtos = cars.Select(car => car.ToDto());
            return OperationResult<IEnumerable<CarDto>>.Success(carDtos);
        }

        // Get car by ID
        public async Task<OperationResult<CarDto>> GetByIdAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                return OperationResult<CarDto>.Failure("Car not found");

            return OperationResult<CarDto>.Success(car.ToDto());
        }

        // Add a new car
        public async Task<OperationResult<CarDto>> AddAsync(CarDto carDto)
        {
            var car = new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Year = carDto.Year,
                DailyPrice = carDto.DailyPrice,
                Color = carDto.Color,
                PlateNumber = carDto.PlateNumber,
                IsAvailable = true ,// Default olarak aracı mevcut kabul edelim
                Description = carDto.Description, 
                CarTypeId = carDto.CarTypeId

                /*Brand = request.Brand,
                    Model = request.Model,
                    Year = request.Year,
                    DailyPrice = request.DailyPrice,
                    Color = request.Color,
                    PlateNumber = request.PlateNumber,
                    IsAvailable = true, // Varsayılan olarak aracın mevcut olduğunu kabul ediyoruz
                    Description = request.Description, // Description'ı buraya ekleyin
                    CarTypeId = request.CarTypeId
    */



            };

            await _carRepository.AddAsync(car);
            return OperationResult<CarDto>.Success(car.ToDto());
        }

        // Update an existing car
        public async Task<OperationResult<CarDto>> UpdateAsync(CarDto carDto)
        {
            var car = await _carRepository.GetByIdAsync(carDto.Id);
            if (car == null)
                return OperationResult<CarDto>.Failure("Car not found");

            car.Brand = carDto.Brand;
            car.Model = carDto.Model;
            car.Year = carDto.Year;
            car.DailyPrice = carDto.DailyPrice;
            car.Color = carDto.Color;
            car.PlateNumber = carDto.PlateNumber;
            car.IsAvailable = carDto.IsAvailable;
            car.Description = carDto.Description;
            car.CarTypeId = carDto.CarTypeId;


            await _carRepository.UpdateAsync(car);
            return OperationResult<CarDto>.Success(car.ToDto());
        }

        // Delete a car
        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                return OperationResult<bool>.Failure("Car not found");

            await _carRepository.DeleteAsync(id);
            return OperationResult<bool>.Success(true);
        }

        // Get available cars
        public async Task<OperationResult<IEnumerable<CarDto>>> GetAvailableCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            var availableCars = cars.Where(car => car.IsAvailable);
            var carDtos = availableCars.Select(car => car.ToDto());
            return OperationResult<IEnumerable<CarDto>>.Success(carDtos);
        }
    }
}
