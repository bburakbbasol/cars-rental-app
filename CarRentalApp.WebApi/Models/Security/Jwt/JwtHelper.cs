using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalApp.WebApi.Security.Jwt
{
    public class JwtHelper
    {
        private readonly JwtConfiguration _configuration;

        public JwtHelper(JwtConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtDto CreateToken(int userId, string email, string name, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimNames.Id, userId.ToString()),
                new Claim(JwtClaimNames.Email, email),
                new Claim(JwtClaimNames.Name, name),
                new Claim(JwtClaimNames.Role, role),
            };

            var expiration = DateTime.Now.AddMinutes(_configuration.ExpirationInMinutes);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
