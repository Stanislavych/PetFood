using FluentValidation;
using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Validators
{
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDtoValidator()
        {
            RuleFor(dto => dto.Username).MinimumLength(5).WithMessage("Username must be at least 5 characters long").MaximumLength(20).WithMessage("Username cannot be longer than 20 characters");
            RuleFor(dto => dto.FirstName).NotEmpty().WithMessage("First name is required").MaximumLength(20).WithMessage("First name cannot be longer than 20 characters");
            RuleFor(dto => dto.PhoneNumber).NotEmpty().WithMessage("Phone number is required").MaximumLength(20).WithMessage("Phone cannot be longer than 16 characters");
        }
    }
}
