using CarRentalApp.Business.Models;
using CarRentalApp.Business.Operations;
using CarRentalApp.WebApi.Models;
using CarRentalApp.WebApi.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarManager _carManager;

        public CarsController(ICarManager carManager)
        {
            _carManager = carManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _carManager.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carManager.GetByIdAsync(id);
            return result.IsSuccessful ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] AddCarRequest request)
        {
            var result = await _carManager.AddAsync(request.ToDto());
            return result.IsSuccessful
                ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result)
                : BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CarDto carDto)
        {
            if (id != carDto.Id)
                return BadRequest("ID uyuşmazlığı");

            var result = await _carManager.UpdateAsync(carDto);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carManager.DeleteAsync(id);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars()
        {
            var result = await _carManager.GetAvailableCarsAsync();
            return Ok(result);
        }
    }
}
