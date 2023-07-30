using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPetsAsync();
        Task<PetDto> AddPetAsync(PetDto petDto);
        Task<PetDto> UpdatePetAsync(PetDto petDto);
        Task<bool> DeletePetAsync(int petId);
    }
}
