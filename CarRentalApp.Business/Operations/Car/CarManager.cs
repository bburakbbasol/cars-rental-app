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

        public async Task<OperationResult<IEnumerable<CarDto>>> GetAllAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            var carDtos = cars.Select(car => car.ToDto());
            return OperationResult<IEnumerable<CarDto>>.Success(carDtos);
        }

        public async Task<OperationResult<CarDto>> GetByIdAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                return OperationResult<CarDto>.Failure("Car not found");

            return OperationResult<CarDto>.Success(car.ToDto());
        }

        public async Task<OperationResult<CarDto>> AddAsync(CarDto carDto)
        {
            var car = carDto.ToEntity();
            await _carRepository.AddAsync(car);
            return OperationResult<CarDto>.Success(car.ToDto());
        }

        public async Task<OperationResult<CarDto>> UpdateAsync(CarDto carDto)
        {
            var car = await _carRepository.GetByIdAsync(carDto.Id);
            if (car == null)
                return OperationResult<CarDto>.Failure("Car not found");

            car.UpdateFromDto(carDto);
            await _carRepository.UpdateAsync(car);
            return OperationResult<CarDto>.Success(car.ToDto());
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                return OperationResult<bool>.Failure("Car not found");

            await _carRepository.DeleteAsync(id);
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<IEnumerable<CarDto>>> GetAvailableCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            var availableCars = cars.Where(car => car.IsAvailable);
            var carDtos = availableCars.Select(car => car.ToDto());
            return OperationResult<IEnumerable<CarDto>>.Success(carDtos);
        }
    }
}
