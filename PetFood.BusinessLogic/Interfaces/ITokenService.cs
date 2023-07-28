using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(UserLoginDto userLogin);
    }
}
