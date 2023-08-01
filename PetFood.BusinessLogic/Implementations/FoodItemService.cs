using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public FoodItemService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<FoodItemDto> AddFoodItemAsync(FoodItemDto FoodItemDto)
        {
            var newFoodItem = _mapper.Map<FoodItem>(FoodItemDto);

            await _repositoryManager.FoodItem.CreateAsync(newFoodItem);
            await _repositoryManager.SaveAsync();

            var createdFoodItem = _mapper.Map<FoodItemDto>(newFoodItem);

            return createdFoodItem;
        }

        public async Task<bool> DeleteFoodItemAsync(int foodItemId)
        {
            var existingFoodItem = (await _repositoryManager.FoodItem.FindByConditionAsync(fi => fi.Id == foodItemId)).SingleOrDefault();

            if (existingFoodItem == null)
            {
                throw new ArgumentException("Food item not found");
            }

            await _repositoryManager.FoodItem.RemoveAsync(existingFoodItem);
            await _repositoryManager.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<FoodItemDto>> GetAllFoodItemsAsync()
        {
            var foodItems = await _repositoryManager.FoodItem.FindAllAsync();
            var foodItemsDto = _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);

            return foodItemsDto;
        }

        public async Task<FoodItemDto> UpdateFoodItemAsync(FoodItemDto foodItemDto)
        {
            var existingFoodType = await _repositoryManager.FoodItem.FindByConditionAsync(fi => fi.Id == foodItemDto.Id);

            if (existingFoodType == null)
            {
                throw new ArgumentException("Food item not found");
            }

            var updatedFoodItem = _mapper.Map<FoodItem>(foodItemDto);

            await _repositoryManager.FoodItem.UpdateAsync(updatedFoodItem);
            await _repositoryManager.SaveAsync();

            var updatedFoodItemDto = _mapper.Map<FoodItemDto>(updatedFoodItem);

            return updatedFoodItemDto;
        }

        public async Task<IEnumerable<FoodItemDto>> GetFoodItemsByPetAndFoodType(int petId,int foodTypeId)
        {
            var foodItems = await _repositoryManager.FoodItem.GetFoodItemByPetAndFoodType(petId, foodTypeId);
            var foodItemsDto = _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);

            return foodItemsDto;
        }

        public async Task<IEnumerable<FoodItemDto>> GetFoodItemsByPet(int petId)
        {
            var foodItems = await _repositoryManager.FoodItem.GetFoodItemByPet(petId);
            var foodItemsDto = _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);

            return foodItemsDto;
        }

        public async Task<IEnumerable<FoodItemDto>> GetFoodItemsByFoodType(int foodTypeId)
        {
            var foodItems = await _repositoryManager.FoodItem.GetFoodItemByFoodType(foodTypeId);
            var foodItemsDto = _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);

            return foodItemsDto;
        }
    }
}
