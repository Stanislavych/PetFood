using FluentValidation;
using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Validators
{
    public class PetDtoValidator : AbstractValidator<PetDto>
    {
        public PetDtoValidator()
        {
            RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name is required");
        }
    }
}
