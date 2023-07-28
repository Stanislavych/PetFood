using Microsoft.AspNetCore.Identity;
using PetFood.DAL.Models;

namespace PetFood.DAL.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> RegisterUserAsync(User userForRegistration);
    }
}
