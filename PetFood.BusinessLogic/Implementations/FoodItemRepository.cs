using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.DatabaseContext;
using PetFood.DAL.GenericRepository.Service;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class FoodItemRepository : RepositoryBase<FoodItem>, IFoodItemRepository
    {
        public FoodItemRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async Task<IEnumerable<FoodItem>> GetFoodItemByFoodType(int foodTypeId)
        {
            return await FindByConditionAsync(fi => fi.FoodTypeId == foodTypeId);
        }

        public async Task<IEnumerable<FoodItem>> GetFoodItemByPet(int petId)
        {
            return await FindByConditionAsync(fi => fi.PetId == petId);
        }

        public async Task<IEnumerable<FoodItem>> GetFoodItemByPetAndFoodType(int petId, int foodTypeId)
        {
            return await FindByConditionAsync(fi => fi.FoodTypeId == foodTypeId && fi.PetId == petId);
        }
    }
}
