namespace PetFood.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllRoles();
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> AssignRoleToUserAsync(string username, string roleName);
        Task<bool> RevokeRoleFromUserAsync(string username, string roleName);
    }
}
