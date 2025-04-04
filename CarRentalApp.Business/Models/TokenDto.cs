namespace CarRentalApp.Business.Models
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
