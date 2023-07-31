namespace PetFood.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllRoles();
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> AssignToleToUserAsync(string username, string roleName);
    }
}
