using CarRentalApp.Business.Models;
using System.Threading.Tasks;

namespace CarRentalApp.Business.Operations
{
    public interface ICarManager
    {
        Task<OperationResult<IEnumerable<CarDto>>> GetAllAsync();
        Task<OperationResult<CarDto>> GetByIdAsync(int id);
        Task<OperationResult<CarDto>> AddAsync(CarDto carDto);
        Task<OperationResult<CarDto>> UpdateAsync(CarDto carDto);
        Task<OperationResult<bool>> DeleteAsync(int id);
        Task<OperationResult<IEnumerable<CarDto>>> GetAvailableCarsAsync();
    }
}
