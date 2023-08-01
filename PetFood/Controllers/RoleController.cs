using Microsoft.AspNetCore.Mvc;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();

            return Ok(roles);
        }

        [HttpPost("{roleName}")]
        public async Task<ActionResult<bool>> CreateRole(string roleName)
        {
            var newRole = await _roleService.CreateRoleAsync(roleName);

            return Ok(newRole);
        }

        [HttpDelete("{roleName}")]
        public async Task<ActionResult<bool>> DeleteRole(string roleName)
        {
            var deletedRole = await _roleService.DeleteRoleAsync(roleName);

            return Ok(deletedRole);
        }

        [HttpPost("assign")]
        public async Task<ActionResult<bool>> AssignRoleToUser(string username, string roleName)
        {
            var result = await _roleService.AssignRoleToUserAsync(username, roleName);

            return Ok(result);
        }

        [HttpPost("revoke")]
        public async Task<ActionResult<bool>> RevokeRoleFromUser(string username, string roleName)
        {
            var result = await _roleService.RevokeRoleFromUserAsync(username, roleName);

            return Ok(result);
        }
    }
}
