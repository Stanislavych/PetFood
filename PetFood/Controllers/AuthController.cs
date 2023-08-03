using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Filters;
using PetFood.BusinessLogic.Interfaces;

namespace PetFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IValidator<UserRegistrationDto> _registrationValidator;
        private readonly IValidator<UserLoginDto> _loginValidator;

        public AuthController(IAuthService authService, ITokenService tokenService, IValidator<UserRegistrationDto> registrationValidator, IValidator<UserLoginDto> loginValidator)
        {
            _authService = authService;
            _tokenService = tokenService;
            _registrationValidator = registrationValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
        {
            var validationResult = await _registrationValidator.ValidateAsync(userRegistration);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var userResult = await _authService.RegisterUserAsync(userRegistration);

            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLogin)
        {
            var validationResult = await _loginValidator.ValidateAsync(userLogin);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return !await _authService.ValidateUserAsync(userLogin) ? Unauthorized() : Ok(new { Token = await _tokenService.CreateTokenAsync(userLogin) });
        }
    }
}
