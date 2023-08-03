using FluentValidation;
using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Validators
{
    public class FoodTypeDtoValidator : AbstractValidator<FoodTypeDto>
    {
        public FoodTypeDtoValidator()
        {
            RuleFor(dto=>dto.Name).NotEmpty().WithMessage("Name is required").MinimumLength(5).WithMessage("Name must be at least 5 characters")
                .MaximumLength(20).WithMessage("Name cannot be longer than 20 characters");
            RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description must be at least 10 characters")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");
        }
    }
}
