using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IFoodTypeService
    {
        Task<IEnumerable<FoodTypeDto>> GetAllFoodTypesAsync();
        Task<FoodTypeDto> AddFoodTypeAsync(FoodTypeDto FoodTypeDto);
        Task<FoodTypeDto> UpdateFoodTypeAsync(FoodTypeDto foodTypeDto);
        Task<bool> DeleteFoodTypeAsync(int foodTypeId);
    }
}
