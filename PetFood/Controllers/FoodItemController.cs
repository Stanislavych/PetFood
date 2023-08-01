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

        public FoodItemController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetAllFoodItems()
        {
            var foodItems = await _foodItemService.GetAllFoodItemsAsync();

            return Ok(foodItems);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<FoodItemDto>> CreateFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            var newFoodItem = await _foodItemService.AddFoodItemAsync(foodItemDto);

            return Ok(newFoodItem);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<FoodItemDto>> UpdateFoodItem([FromBody] FoodItemDto foodItemDto)
        {
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
