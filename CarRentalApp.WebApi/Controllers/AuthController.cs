using CarRentalApp.Business.Services.Mapper;
using CarRentalApp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using CarRentalApp.WebApi.Models.Auth;

namespace CarRentalApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuthMapper _authMapper;

        public AuthController(IAuthService authService, IAuthMapper authMapper)
        {
            _authService = authService;
            _authMapper = authMapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Şifreler eşleşmiyor.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Hatalı alanlar döner
            }

            var registerDto = _authMapper.MapToRegisterDto(request);
            var result = await _authService.RegisterAsync(registerDto);
            return result ? Ok("Kayıt başarılı.") : BadRequest("Kayıt başarısız.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var loginDto = _authMapper.MapToLoginDto(request); // Request'i DTO'ya dönüştür
                var token = await _authService.LoginAsync(loginDto);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("AuthController is working.");
        }
    }
}
