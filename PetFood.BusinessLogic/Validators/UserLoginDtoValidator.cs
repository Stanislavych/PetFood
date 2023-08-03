using FluentValidation;
using PetFood.BusinessLogic.Dto;

namespace PetFood.BusinessLogic.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(dto=>dto.Username).MinimumLength(5).WithMessage("Username must be at least 5 characters long").MaximumLength(20).WithMessage("Username cannot be longer than 20 characters");
        }
    }
}
