using System.ComponentModel.DataAnnotations;

namespace PetFood.BusinessLogic.Dto
{
    public class UserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Roles { get; set; }
    }
}
