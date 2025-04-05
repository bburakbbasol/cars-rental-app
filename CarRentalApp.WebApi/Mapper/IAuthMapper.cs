using CarRentalApp.Business.Models;
using CarRentalApp.WebApi.Models.Auth;
namespace CarRentalApp.Business.Services.Mapper
{
    public interface IAuthMapper
    {
        RegisterDto MapToRegisterDto(RegisterRequest request);
        LoginDto MapToLoginDto(LoginRequest request);
    }
}
