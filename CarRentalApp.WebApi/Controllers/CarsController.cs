using CarRentalApp.Business.Models;
using CarRentalApp.Business.Operations;
using CarRentalApp.WebApi.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRentalApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    //Authorize]  // Yalnızca yetkilendirilmiş kullanıcılar erişebilir
    public class CarsController : ControllerBase
    {
        private readonly ICarManager _carManager;

        public CarsController(ICarManager carManager)
        {
            _carManager = carManager;
        }

        // POST: api/cars
        [HttpPost]
        [Authorize(Roles ="Admin")] // Yalnızca yetkilendirilmiş kullanıcılar erişebilir
        public async Task<IActionResult> AddCar([FromBody] AddCarRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Invalid car data." });
            }

            // CarDto'ya dönüştürülmüş request objesi
            var carDto = new CarDto
            {
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                DailyPrice = request.DailyPrice,
                Color = request.Color,
                PlateNumber = request.PlateNumber,
                IsAvailable = true, // Varsayılan olarak aracın mevcut olduğunu kabul ediyoruz
                Description = request.Description, // Description'ı buraya ekleyin
                CarTypeId = request.CarTypeId
            };

            var result = await _carManager.AddAsync(carDto);

            if (result.IsSuccessful)
            {
                // Yeni oluşturulan aracı döndür
                return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
            }
            else
            {
                return BadRequest(new { Message = result.ErrorMessage });
            }
        }

        // GET: api/cars/5
        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carManager.GetByIdAsync(id);
            if (result.IsSuccessful)
            {
                return Ok(result.Data);
            }
            return NotFound(new { Message = result.ErrorMessage });
        }


        // GET: api/cars
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _carManager.GetAllAsync();
            if (result.IsSuccessful)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { Message = result.ErrorMessage });
        }

        // PUT: api/cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarRequest request)
        {
            if (request == null || id != request.Id)
            {
                return BadRequest(new { Message = "Invalid car data." });
            }

            var carDto = new CarDto
            {
                Id = request.Id,
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                DailyPrice = request.DailyPrice,
                Color = request.Color,
                PlateNumber = request.PlateNumber,
                IsAvailable = request.IsAvailable,
                Description = request.Description,
                CarTypeId = request.CarTypeId
            };

            var result = await _carManager.UpdateAsync(carDto);

            if (result.IsSuccessful)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(new { Message = result.ErrorMessage });
            }
        }

        // DELETE: api/cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carManager.DeleteAsync(id);
            if (result.IsSuccessful)
            {
                return Ok(new { Message = "Car deleted successfully." });
            }
            return BadRequest(new { Message = result.ErrorMessage });
        }

        // GET: api/cars/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars()
        {
            var result = await _carManager.GetAvailableCarsAsync();
            if (result.IsSuccessful)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { Message = result.ErrorMessage });
        }
    }
}

