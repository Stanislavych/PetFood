using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class FoodTypeService : IFoodTypeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public FoodTypeService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<FoodTypeDto> AddFoodTypeAsync(FoodTypeDto FoodTypeDto)
        {
            var newFoodType = _mapper.Map<FoodType>(FoodTypeDto);

            await _repositoryManager.FoodType.CreateAsync(newFoodType);
            await _repositoryManager.SaveAsync();

            var createdFoodType = _mapper.Map<FoodTypeDto>(newFoodType);

            return createdFoodType;
        }

        public async Task<bool> DeleteFoodTypeAsync(int foodTypeId)
        {
            var existingFoodType = (await _repositoryManager.FoodType.FindByConditionAsync(ft => ft.Id == foodTypeId)).SingleOrDefault();

            if (existingFoodType==null)
            {
                return false;
            }

            await _repositoryManager.FoodType.RemoveAsync(existingFoodType);
            await _repositoryManager.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<FoodTypeDto>> GetAllFoodTypesAsync()
        {
            var foodTypes = await _repositoryManager.FoodType.FindAllAsync();
            var foodTypesDto = _mapper.Map<IEnumerable<FoodTypeDto>>(foodTypes);

            return foodTypesDto;
        }

        public async Task<FoodTypeDto> UpdateFoodTypeAsync(FoodTypeDto foodTypeDto)
        {
            var existingFoodType = (await _repositoryManager.FoodType.FindByConditionAsync(ft=>ft.Id==foodTypeDto.Id)).SingleOrDefault();

            if (existingFoodType == null)
            {
                throw new ArgumentNullException("Food type not found");
            }

            var updatedFoodType = _mapper.Map<FoodType>(foodTypeDto);

            await _repositoryManager.FoodType.UpdateAsync(updatedFoodType);
            await _repositoryManager.SaveAsync();

            var updatedFoodTypeDto = _mapper.Map<FoodTypeDto>(updatedFoodType);

            return updatedFoodTypeDto;
        }
    }
}
