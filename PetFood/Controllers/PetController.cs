using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Filters;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IValidator<PetDto> _validator;

        public PetController(IPetService petService, IValidator<PetDto> validator)
        {
            _petService = petService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetAllPets()
        {
            var petsDto = await _petService.GetAllPetsAsync();

            return Ok(petsDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<PetDto>> CreatePet([FromBody] PetDto petDto)
        {
            var validationResult = await _validator.ValidateAsync(petDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newPet = await _petService.AddPetAsync(petDto);

            return Ok(newPet);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<PetDto>> UpdatePet([FromBody] PetDto petDto)
        {
            var validationResult = await _validator.ValidateAsync(petDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedPet = await _petService.UpdatePetAsync(petDto);

            return Ok(updatedPet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePet(int id)
        {
            var result = await _petService.DeletePetAsync(id);

            return Ok(result);
        }
    }
}
