using AutoMapper;
using FluentAssertions;
using Moq;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Implementations;
using PetFood.BusinessLogic.Interfaces;
using PetFood.BusinessLogic.Mappings;
using PetFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PetFood.Tests.Services
{
    public class FoodTypeServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FoodTypeService _foodTypeService;

        public FoodTypeServiceTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new FoodTypeMappingProfile()));
            _mapperMock = new Mock<IMapper>();
            _foodTypeService = new FoodTypeService(_repositoryManagerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddFoodTypeAsync_ShouldCreateAndReturnFoodTypeDto()
        {
            // Arrange
            var foodTypeDto = new FoodTypeDto
            {
                Name = "Dry Food",
                Description = "Dry food for pets"
            };

            var foodType = new FoodType
            {
                Name = foodTypeDto.Name,
                Description = foodTypeDto.Description
            };

            _mapperMock.Setup(mapper => mapper.Map<FoodType>(foodTypeDto)).Returns(foodType);
            _mapperMock.Setup(mapper => mapper.Map<FoodTypeDto>(foodType)).Returns(foodTypeDto);

            _repositoryManagerMock.Setup(repo => repo.FoodType.CreateAsync(It.IsAny<FoodType>())).Callback<FoodType>(ft =>
            {
                // Set the Id for the foodType to simulate successful saving in the repository
                foodType.Id = 1;
            });

            // Act
            var result = await _foodTypeService.AddFoodTypeAsync(foodTypeDto);

            // Assert
            result.Should().BeEquivalentTo(foodTypeDto);

            _repositoryManagerMock.Verify(repo => repo.FoodType.CreateAsync(It.IsAny<FoodType>()), Times.Once);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_ShouldDeleteFoodTypeAndReturnTrue()
        {
            // Arrange
            var foodTypeId = 1;

            var existingFoodType = new FoodType
            {
                Id = foodTypeId,
                Name = "Dry Food",
                Description = "Dry food for pets"
            };

            _repositoryManagerMock.Setup(repo => repo.FoodType.FindByConditionAsync(ft => ft.Id == foodTypeId)).ReturnsAsync(new List<FoodType> { existingFoodType });

            // Act
            var result = await _foodTypeService.DeleteFoodTypeAsync(foodTypeId);

            // Assert
            result.Should().BeTrue();

            _repositoryManagerMock.Verify(repo => repo.FoodType.RemoveAsync(existingFoodType), Times.Once);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllFoodTypesAsync_ShouldReturnAllFoodTypes()
        {
            // Arrange
            var foodTypes = new List<FoodType>
            {
                new FoodType { Id = 1, Name = "Dry Food", Description = "Dry food for pets" },
                new FoodType { Id = 2, Name = "Wet Food", Description = "Wet food for pets" },
            };

            var foodTypesDto = foodTypes.Select(ft => new FoodTypeDto
            {
                Id = ft.Id,
                Name = ft.Name,
                Description = ft.Description
            });

            _repositoryManagerMock.Setup(repo => repo.FoodType.FindAllAsync()).ReturnsAsync(foodTypes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<FoodTypeDto>>(foodTypes)).Returns(foodTypesDto);

            // Act
            var result = await _foodTypeService.GetAllFoodTypesAsync();

            // Assert
            result.Should().BeEquivalentTo(foodTypesDto);
        }

        [Fact]
        public async Task UpdateFoodTypeAsync_ShouldUpdateAndReturnFoodTypeDto()
        {
            // Arrange
            var foodTypeDto = new FoodTypeDto
            {
                Id = 1,
                Name = "Updated Food Type",
                Description = "Updated description"
            };

            var existingFoodType = new FoodType
            {
                Id = foodTypeDto.Id,
                Name = "Old Food Type",
                Description = "Old description"
            };

            var updatedFoodType = new FoodType
            {
                Id = foodTypeDto.Id,
                Name = foodTypeDto.Name,
                Description = foodTypeDto.Description
            };

            _repositoryManagerMock.Setup(repo => repo.FoodType.FindByConditionAsync(ft => ft.Id == foodTypeDto.Id)).ReturnsAsync(new List<FoodType> { existingFoodType });
            _mapperMock.Setup(mapper => mapper.Map<FoodType>(foodTypeDto)).Returns(updatedFoodType);
            _mapperMock.Setup(mapper => mapper.Map<FoodTypeDto>(updatedFoodType)).Returns(foodTypeDto);

            // Act
            var result = await _foodTypeService.UpdateFoodTypeAsync(foodTypeDto);

            // Assert
            result.Should().BeEquivalentTo(foodTypeDto);

            _repositoryManagerMock.Verify(repo => repo.FoodType.UpdateAsync(updatedFoodType), Times.Once);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }
    }
}
