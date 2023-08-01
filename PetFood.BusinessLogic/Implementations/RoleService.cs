using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AssignRoleToUserAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (roleExist)
            {
                throw new ArgumentException("Role already exist");
            }

            var newRole = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(newRole);

            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                throw new ArgumentException("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return roles;
        }

        public async Task<bool> RevokeRoleFromUserAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            return result.Succeeded;
        }
    }
}
