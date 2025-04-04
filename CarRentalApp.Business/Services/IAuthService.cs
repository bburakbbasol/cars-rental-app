using CarRentalApp.Business.Models;

namespace CarRentalApp.Business.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
    }
}
