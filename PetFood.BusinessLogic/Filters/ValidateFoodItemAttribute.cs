using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.BusinessLogic.Filters
{
    public class ValidateFoodItemAttribute : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        private readonly IRepositoryManager _repositoryManager;

        public ValidateFoodItemAttribute(ILogger logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("foodItemDto"))
            {
                var foodItemDto = context.ActionArguments["foodItemDto"] as FoodItemDto;

                if (foodItemDto != null)
                {
                    await ValidateFoodItemByPetId(foodItemDto.PetId, context);
                    await ValidateFoodItemByFoodTypeId(foodItemDto.FoodTypeId, context);

                    if (context.Result != null)
                    {
                        return;
                    }
                }
            }

            if (context.ActionArguments.ContainsKey("petId"))
            {
                if (int.TryParse(context.ActionArguments["petId"].ToString(), out int petId))
                {
                    await ValidateFoodItemByPetId(petId, context);

                    if (context.Result != null)
                    {
                        return;
                    }
                }
            }

            if (context.ActionArguments.ContainsKey("foodTypeId"))
            {
                if (int.TryParse(context.ActionArguments["foodTypeId"].ToString(), out int foodTypeId))
                {
                    await ValidateFoodItemByFoodTypeId(foodTypeId, context);

                    if (context.Result != null)
                    {
                        return;
                    }
                }
            }

            await next();
        }

        private async Task ValidateFoodItemByPetId(int petId, ActionExecutingContext context)
        {
            var petExists = await _repositoryManager.Pet.FindByIdAsync(petId);

            if (petExists is null)
            {
                _logger.LogError($"PetId sent from client is invalid: {petId}. Pet with this Id does not exist.");

                context.Result = new BadRequestObjectResult($"Invalid PetId: {petId}. Pet with this Id does not exist.");

                return;
            }
        }

        private async Task ValidateFoodItemByFoodTypeId(int foodTypeId, ActionExecutingContext context)
        {
            var foodTypeExists = await _repositoryManager.FoodType.FindByIdAsync(foodTypeId);

            if (foodTypeExists is null)
            {
                _logger.LogError($"FoodTypeId sent from client is invalid: {foodTypeId}. Food type with this Id does not exist.");

                context.Result = new BadRequestObjectResult($"Invalid FoodTypeId: {foodTypeId}. FoodType with this Id does not exist.");

                return;
            }
        }
    }
}
