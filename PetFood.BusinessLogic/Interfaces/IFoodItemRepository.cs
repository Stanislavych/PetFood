using PetFood.DAL.GenericRepository.Interface;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IFoodItemRepository : IRepositoryBase<FoodItem>
    {
        Task<IEnumerable<FoodItem>> GetFoodItemByPetAndFoodType(int petId, int foodTypeId);
        Task<IEnumerable<FoodItem>> GetFoodItemByPet(int petId);
        Task<IEnumerable<FoodItem>> GetFoodItemByFoodType(int foodTypeId);
    }
}
