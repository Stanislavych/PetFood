using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IFoodItemService
    {
        Task<IEnumerable<FoodItemDto>> GetAllFoodItemsAsync();
        Task<FoodItemDto> AddFoodItemAsync(FoodItemDto FoodItemDto);
        Task<FoodItemDto> UpdateFoodItemAsync(FoodItemDto foodItemDto);
        Task<bool> DeleteFoodItemAsync(int foodItemId);
        Task<IEnumerable<FoodItemDto>> GetFoodItemsByPetAndFoodType(int petId, int foodTypeId);
        Task<IEnumerable<FoodItemDto>> GetFoodItemsByPet(int petId);
        Task<IEnumerable<FoodItemDto>> GetFoodItemsByFoodType(int foodTypeId);
    }
}
