using Microsoft.AspNetCore.Mvc;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Filters;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : ControllerBase
    {
        private readonly IFoodTypeService _foodTypeService;

        public FoodTypeController(IFoodTypeService foodTypeService)
        {
            _foodTypeService = foodTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTypeDto>>> GetAllFoodTypes()
        {
            var foodTypes = await _foodTypeService.GetAllFoodTypesAsync();

            return Ok(foodTypes);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<FoodTypeDto>> CreateFoodType(FoodTypeDto foodTypeDto)
        {
            var newFoodType = await _foodTypeService.AddFoodTypeAsync(foodTypeDto);

            return Ok(newFoodType);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<FoodTypeDto>> UpdateFoodType(FoodTypeDto foodTypeDto)
        {
            var updatedFoodType = await _foodTypeService.UpdateFoodTypeAsync(foodTypeDto);

            return Ok(updatedFoodType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteFoodType(int id)
        {
            await _foodTypeService.DeleteFoodTypeAsync(id);

            return Ok("Food type with id - " + id + " was deleted");
        }
    }
}
