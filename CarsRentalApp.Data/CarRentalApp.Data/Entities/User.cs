using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.Data.Entities
{
    public class User : IdentityUser
    {
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
