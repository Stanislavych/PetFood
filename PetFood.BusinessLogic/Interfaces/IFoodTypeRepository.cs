using PetFood.DAL.GenericRepository.Interface;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IFoodTypeRepository : IRepositoryBase<FoodType>
    {
        Task<FoodType> FindByIdAsync(int id);
    }
}
