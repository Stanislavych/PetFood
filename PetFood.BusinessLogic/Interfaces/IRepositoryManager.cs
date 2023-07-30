
namespace PetFood.BusinessLogic.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }
        IPetRepository Pet { get; }
        IFoodItemRepository FoodItem { get; }
        IFoodTypeRepository FoodType { get; }
        Task SaveAsync();
    }
}
