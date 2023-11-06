using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.DatabaseContext;
using PetFood.DAL.GenericRepository.Service;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class FoodTypeRepository : RepositoryBase<FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(DatabaseContext context) : base(context)
        {

        }

        public async Task<FoodType> FindByIdAsync(int id)
        {
            var foodType = await _context.FoodTypes.FindAsync(id);

            return foodType;
        }
    }
}
