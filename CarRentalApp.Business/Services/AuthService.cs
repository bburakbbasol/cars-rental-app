using CarRentalApp.Business.Jwt;
using CarRentalApp.Business.Models;
using CarRentalApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CarRentalApp.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; // RoleManager ekledik
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, // RoleManager'� constructor'a ekliyoruz
            IConfiguration configuration,
            JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;  // RoleManager'� buraya at�yoruz
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            // Rol� kontrol et, yoksa olu�tur
            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));
            }

            // Kullan�c�ya rol ata
            await _userManager.AddToRoleAsync(user, dto.Role);

            return true;
        }

      public async Task<string> LoginAsync(LoginDto loginDto)
{
    var user = await _userManager.FindByEmailAsync(loginDto.Email);
    if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        throw new UnauthorizedAccessException();

    // Kullan�c�n�n rollerini almak
    var roles = await _userManager.GetRolesAsync(user);  // Kullan�c�n�n rollerini al�yoruz

    // JWT token'� olu�tur
    return _jwtHelper.CreateToken(user, roles);  // Kullan�c� bilgileri ve roller ile token olu�turuyoruz
}

    }
}