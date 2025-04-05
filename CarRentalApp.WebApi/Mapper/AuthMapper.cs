using CarRentalApp.Business.Models;
using CarRentalApp.Business.Services.Mapper;
using CarRentalApp.WebApi.Models.Auth;

namespace CarRentalApp.WebApi.Mapper
{

    public class AuthMapper : IAuthMapper
    {
        public RegisterDto MapToRegisterDto(RegisterRequest request)
        {
            return new RegisterDto
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role
            };
        }
        public LoginDto MapToLoginDto(LoginRequest request)
        {
            return new LoginDto
            {
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}
