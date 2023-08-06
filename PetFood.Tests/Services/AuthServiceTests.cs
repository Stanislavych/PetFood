using Microsoft.AspNetCore.Identity;
using Moq;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Implementations;
using PetFood.BusinessLogic.Interfaces;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace PetFood.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserAuthenticationRepository> _authRepositoryMock;
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IUserAuthenticationRepository>();
            _repositoryMock = new Mock<IRepositoryManager>();

            _repositoryMock.SetupGet(repo => repo.UserAuthentication).Returns(_authRepositoryMock.Object);

            _authService = new AuthService(_repositoryMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldCallRegisterUserAsyncOnRepository()
        {
            // Arrange
            var userRegistration = new UserRegistrationDto
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Password = "password",
                Email = "john.doe@example.com",
                PhoneNumber = "+123456789"
            };

            _authRepositoryMock.Setup(repo => repo.RegisterUserAsync(userRegistration)).ReturnsAsync(IdentityResult.Success);

            // Act
            await _authService.RegisterUserAsync(userRegistration);

            // Assert
            _authRepositoryMock.Verify(repo => repo.RegisterUserAsync(userRegistration), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnIdentityResultFromRepository()
        {
            // Arrange
            var userRegistration = new UserRegistrationDto
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Password = "password",
                Email = "john.doe@example.com",
                PhoneNumber = "+123456789"
            };

            var expectedIdentityResult = IdentityResult.Success;

            _authRepositoryMock.Setup(repo => repo.RegisterUserAsync(userRegistration)).ReturnsAsync(expectedIdentityResult);

            // Act
            var result = await _authService.RegisterUserAsync(userRegistration);

            // Assert
            result.Should().BeEquivalentTo(expectedIdentityResult);
        }

        [Fact]
        public async Task ValidateUserAsync_ShouldCallValidateUserAsyncOnRepository()
        {
            // Arrange
            var userLogin = new UserLoginDto
            {
                Username = "johndoe",
                Password = "password"
            };

            _authRepositoryMock.Setup(repo => repo.ValidateUserAsync(userLogin)).ReturnsAsync(true);

            // Act
            await _authService.ValidateUserAsync(userLogin);

            // Assert
            _authRepositoryMock.Verify(repo => repo.ValidateUserAsync(userLogin), Times.Once);
        }

        [Fact]
        public async Task ValidateUserAsync_ShouldReturnResultFromRepository()
        {
            // Arrange
            var userLogin = new UserLoginDto
            {
                Username = "johndoe",
                Password = "password"
            };

            var expectedResult = true;

            _authRepositoryMock.Setup(repo => repo.ValidateUserAsync(userLogin)).ReturnsAsync(expectedResult);

            // Act
            var result = await _authService.ValidateUserAsync(userLogin);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
