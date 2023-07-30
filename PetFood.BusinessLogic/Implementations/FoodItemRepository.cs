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
    }
}
