using Microsoft.AspNetCore.Identity;

namespace PetFood.DAL.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
