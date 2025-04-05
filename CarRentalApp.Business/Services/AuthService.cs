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
            RoleManager<IdentityRole> roleManager, // RoleManager'ý constructor'a ekliyoruz
            IConfiguration configuration,
            JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;  // RoleManager'ý buraya atýyoruz
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

            // Rolü kontrol et, yoksa oluþtur
            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));
            }

            // Kullanýcýya rol ata
            await _userManager.AddToRoleAsync(user, dto.Role);

            return true;
        }

      public async Task<string> LoginAsync(LoginDto loginDto)
{
    var user = await _userManager.FindByEmailAsync(loginDto.Email);
    if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        throw new UnauthorizedAccessException();

    // Kullanýcýnýn rollerini almak
    var roles = await _userManager.GetRolesAsync(user);  // Kullanýcýnýn rollerini alýyoruz

    // JWT token'ý oluþtur
    return _jwtHelper.CreateToken(user, roles);  // Kullanýcý bilgileri ve roller ile token oluþturuyoruz
}

    }
}