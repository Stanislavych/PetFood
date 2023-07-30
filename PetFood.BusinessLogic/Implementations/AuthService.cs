using Microsoft.AspNetCore.Identity;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repository;

        public AuthService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration)
        {
            var result = _repository.UserAuthentication.RegisterUserAsync(userRegistration);

            return result;
        }

        public Task<bool> ValidateUserAsync(UserLoginDto userLogin)
        {
            var result = _repository.UserAuthentication.ValidateUserAsync(userLogin);

            return result;
        }
    }
}
