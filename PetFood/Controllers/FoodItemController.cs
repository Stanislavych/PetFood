using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Filters;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;
        private readonly IValidator<FoodItemDto> _validator;

        public FoodItemController(IFoodItemService foodItemService, IValidator<FoodItemDto> validator)
        {
            _foodItemService = foodItemService;
            _validator = validator;
        }

        [HttpGet("AllFoodItems")]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetAllFoodItems()
        {
            var foodItems = await _foodItemService.GetAllFoodItemsAsync();

            return Ok(foodItems);
        }

        [HttpGet("GetByPetAndFoodType")]
        [ServiceFilter(typeof(ValidateFoodItemAttribute))]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetFoodItemsByPetAndFoodType(int petId, int foodTypeId)
        {
            var foodItems = await _foodItemService.GetFoodItemsByPetAndFoodType(petId, foodTypeId);

            return Ok(foodItems);
        }

        [HttpGet("GetByPet")]
        [ServiceFilter(typeof(ValidateFoodItemAttribute))]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetFoodItemsByPet(int petId)
        {
            var foodItems = await _foodItemService.GetFoodItemsByPet(petId);

            return Ok(foodItems);
        }

        [HttpGet("GetByFoodType")]
        [ServiceFilter(typeof(ValidateFoodItemAttribute))]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetFoodItemsByFoodType(int foodTypeId)
        {
            var foodItems = await _foodItemService.GetFoodItemsByFoodType(foodTypeId);

            return Ok(foodItems);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFoodItemAttribute))]
        public async Task<ActionResult<FoodItemDto>> CreateFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            var validationResult = await _validator.ValidateAsync(foodItemDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newFoodItem = await _foodItemService.AddFoodItemAsync(foodItemDto);

            return Ok(newFoodItem);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFoodItemAttribute))]
        public async Task<ActionResult<FoodItemDto>> UpdateFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            var validationResult = await _validator.ValidateAsync(foodItemDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedFoodItem = await _foodItemService.UpdateFoodItemAsync(foodItemDto);

            return Ok(updatedFoodItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteFoodItem(int id)
        {
            var result = await _foodItemService.DeleteFoodItemAsync(id);

            return Ok(result);
        }
    }
}
