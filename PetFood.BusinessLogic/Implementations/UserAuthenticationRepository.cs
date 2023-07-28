using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        private User? _user;

        public UserAuthenticationRepository(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration)
        {
            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            return result;
        }

        public async Task<bool> ValidateUserAsync(UserLoginDto userLogin)
        {
            _user = await _userManager.FindByNameAsync(userLogin.Username);

            var result = _user != null && await _userManager.CheckPasswordAsync(_user, userLogin.Password);

            return result;
        }
    }
}
