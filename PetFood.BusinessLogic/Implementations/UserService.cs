using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> DeleteUserAsync(string username)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser==null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(existingUser);

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);

                userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();

                usersDto.Add(userDto);
            }

            return usersDto;
        }
    }
}
