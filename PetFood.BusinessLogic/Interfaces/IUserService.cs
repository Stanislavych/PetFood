using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string username);
    }
}
