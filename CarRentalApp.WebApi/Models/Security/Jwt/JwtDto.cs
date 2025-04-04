namespace CarRentalApp.WebApi.Security.Jwt
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class JwtConfiguration
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
