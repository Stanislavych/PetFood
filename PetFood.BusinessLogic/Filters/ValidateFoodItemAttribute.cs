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
            var foodItemDto = context.ActionArguments["foodItemDto"] as FoodItemDto;

            if (foodItemDto != null)
            {
                var petExists = await _repositoryManager.Pet.FindByIdAsync(foodItemDto.PetId);
                var foodTypeExists = await _repositoryManager.FoodType.FindByIdAsync(foodItemDto.FoodTypeId);

                if (petExists is null)
                {
                    _logger.LogError($"PetId sent from client is invalid: {foodItemDto.PetId}. Pet with this Id does not exist.");

                    context.Result = new BadRequestObjectResult($"Invalid PetId: {foodItemDto.PetId}. Pet with this Id does not exist.");
                    return;
                }

                if (foodTypeExists is null)
                {
                    _logger.LogError($"FoodTypeId sent from client is invalid: {foodItemDto.FoodTypeId}. Food type with this Id does not exist.");

                    context.Result = new BadRequestObjectResult($"Invalid FoodTypeId: {foodItemDto.FoodTypeId}. FoodType with this Id does not exist.");
                    return;
                }
            }

            await next();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
