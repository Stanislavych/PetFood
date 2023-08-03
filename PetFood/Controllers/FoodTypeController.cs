using FluentValidation;
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
        private readonly IValidator<FoodTypeDto> _validator;

        public FoodTypeController(IFoodTypeService foodTypeService, IValidator<FoodTypeDto> validator)
        {
            _foodTypeService = foodTypeService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTypeDto>>> GetAllFoodTypes()
        {
            var foodTypes = await _foodTypeService.GetAllFoodTypesAsync();

            return Ok(foodTypes);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<FoodTypeDto>> CreateFoodType([FromBody] FoodTypeDto foodTypeDto)
        {
            var validationResult = await _validator.ValidateAsync(foodTypeDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newFoodType = await _foodTypeService.AddFoodTypeAsync(foodTypeDto);

            return Ok(newFoodType);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<FoodTypeDto>> UpdateFoodType([FromBody] FoodTypeDto foodTypeDto)
        {
            var validationResult = await _validator.ValidateAsync(foodTypeDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedFoodType = await _foodTypeService.UpdateFoodTypeAsync(foodTypeDto);

            return Ok(updatedFoodType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteFoodType(int id)
        {
            var result = await _foodTypeService.DeleteFoodTypeAsync(id);

            return Ok(result);
        }
    }
}
