using Microsoft.AspNetCore.Identity;
using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration);
        Task<bool> ValidateUserAsync(UserLoginDto userLogin);
    }
}
