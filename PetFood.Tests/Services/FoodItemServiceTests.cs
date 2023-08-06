using AutoMapper;
using FluentAssertions;
using Moq;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Implementations;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PetFood.Tests.Services
{
    public class FoodItemServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FoodItemService _foodItemService;

        public FoodItemServiceTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            _foodItemService = new FoodItemService(_repositoryManagerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddFoodItemAsync_ShouldCreateAndReturnFoodItemDto()
        {
            // Arrange
            var foodItemDto = new FoodItemDto
            {
                Name = "Dog Food",
                Description = "Delicious dog food",
                Price = 12.99,
                PetId = 1,
                FoodTypeId = 2
            };

            var foodItem = new FoodItem
            {
                Name = foodItemDto.Name,
                Description = foodItemDto.Description,
                Price = foodItemDto.Price,
                PetId = foodItemDto.PetId,
                FoodTypeId = foodItemDto.FoodTypeId
            };

            _mapperMock.Setup(mapper => mapper.Map<FoodItem>(foodItemDto)).Returns(foodItem);
            _mapperMock.Setup(mapper => mapper.Map<FoodItemDto>(foodItem)).Returns(foodItemDto);

            _repositoryManagerMock.Setup(repo => repo.FoodItem.CreateAsync(It.IsAny<FoodItem>())).Callback<FoodItem>(fi =>
            {
                foodItem.Id = 1;
            });

            // Act
            var result = await _foodItemService.AddFoodItemAsync(foodItemDto);

            // Assert
            result.Should().BeEquivalentTo(foodItemDto);

            _repositoryManagerMock.Verify(repo => repo.FoodItem.CreateAsync(foodItem), Times.Once);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteFoodItemAsync_ShouldDeleteFoodItemAndReturnTrue()
        {
            // Arrange
            var foodItemId = 1;
            var existingFoodItem = new FoodItem
            {
                Id = foodItemId,
                Name = "Dog Food",
                Description = "Delicious dog food",
                Price = 12.99,
                PetId = 1,
                FoodTypeId = 2
            };

            _repositoryManagerMock.Setup(repo => repo.FoodItem.FindByConditionAsync(fi => fi.Id == foodItemId)).ReturnsAsync(new List<FoodItem> { existingFoodItem });

            // Act
            var result = await _foodItemService.DeleteFoodItemAsync(foodItemId);

            // Assert
            result.Should().BeTrue();

            _repositoryManagerMock.Verify(repo => repo.FoodItem.RemoveAsync(existingFoodItem), Times.Once);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteFoodItemAsync_ShouldThrowArgumentException_WhenFoodItemNotFound()
        {
            // Arrange
            var foodItemId = 1;

            _repositoryManagerMock.Setup(repo => repo.FoodItem.FindByConditionAsync(fi => fi.Id == foodItemId)).ReturnsAsync(new List<FoodItem>());

            // Act
            Func<Task> act = async () => await _foodItemService.DeleteFoodItemAsync(foodItemId);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Food item not found");

            _repositoryManagerMock.Verify(repo => repo.FoodItem.RemoveAsync(It.IsAny<FoodItem>()), Times.Never);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateFoodItemAsync_ShouldUpdateAndReturnFoodItemDto()
        {
            // Arrange
            var foodItemDto = new FoodItemDto
            {
                Id = 1,
                Name = "Dog Food",
                Description = "Delicious dog food",
                Price = 15.99,
                PetId = 1,
                FoodTypeId = 2
            };

            var existingFoodItem = new FoodItem
            {
                Id = foodItemDto.Id,
                Name = "Old Food",
                Description = "Old description",
                Price = 9.99,
                PetId = 1,
                FoodTypeId = 2
            };

            var updatedFoodItem = new FoodItem
            {
                Id = foodItemDto.Id,
                Name = foodItemDto.Name,
                Description = foodItemDto.Description,
                Price = foodItemDto.Price,
                PetId = foodItemDto.PetId,
                FoodTypeId = foodItemDto.FoodTypeId
            };

            _repositoryManagerMock.Setup(repo => repo.FoodItem.FindByConditionAsync(fi => fi.Id == foodItemDto.Id)).ReturnsAsync(new List<FoodItem> { existingFoodItem });
            _mapperMock.Setup(mapper => mapper.Map<FoodItem>(foodItemDto)).Returns(updatedFoodItem);
            _mapperMock.Setup(mapper => mapper.Map<FoodItemDto>(updatedFoodItem)).Returns(foodItemDto);

            // Act
            var result = await _foodItemService.UpdateFoodItemAsync(foodItemDto);

            // Assert
            result.Should().BeEquivalentTo(foodItemDto);

            _repositoryManagerMock.Verify(repo => repo.FoodItem.UpdateAsync(updatedFoodItem), Times.Once);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateFoodItemAsync_ShouldThrowArgumentException_WhenFoodItemNotFound()
        {
            // Arrange
            var foodItemDto = new FoodItemDto
            {
                Id = 1,
                Name = "Dog Food",
                Description = "Delicious dog food",
                Price = 15.99,
                PetId = 1,
                FoodTypeId = 2
            };

            _repositoryManagerMock.Setup(repo => repo.FoodItem.FindByConditionAsync(fi => fi.Id == foodItemDto.Id)).ReturnsAsync((IEnumerable<FoodItem>)null);

            // Act
            Func<Task> act = async () => await _foodItemService.UpdateFoodItemAsync(foodItemDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Food item not found");

            _repositoryManagerMock.Verify(repo => repo.FoodItem.UpdateAsync(It.IsAny<FoodItem>()), Times.Never);
            _repositoryManagerMock.Verify(repo => repo.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAllFoodItemsAsync_ShouldReturnAllFoodItems()
        {
            // Arrange
            var foodItems = new List<FoodItem>
            {
                new FoodItem { Id = 1, Name = "Food 1", Description = "Description 1", Price = 10.0, PetId = 1, FoodTypeId = 1 },
                new FoodItem { Id = 2, Name = "Food 2", Description = "Description 2", Price = 15.0, PetId = 2, FoodTypeId = 1 },
                new FoodItem { Id = 3, Name = "Food 3", Description = "Description 3", Price = 12.5, PetId = 1, FoodTypeId = 2 },
            };

            var foodItemsDto = foodItems.Select(fi => new FoodItemDto
            {
                Id = fi.Id,
                Name = fi.Name,
                Description = fi.Description,
                Price = fi.Price,
                PetId = fi.PetId,
                FoodTypeId = fi.FoodTypeId
            });

            _repositoryManagerMock.Setup(repo => repo.FoodItem.FindAllAsync()).ReturnsAsync(foodItems);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<FoodItemDto>>(foodItems)).Returns(foodItemsDto);

            // Act
            var result = await _foodItemService.GetAllFoodItemsAsync();

            // Assert
            result.Should().BeEquivalentTo(foodItemsDto);
        }

        [Fact]
        public async Task GetFoodItemsByPetAndFoodType_ShouldReturnMatchingFoodItems()
        {
            // Arrange
            var petId = 1;
            var foodTypeId = 2;

            var foodItems = new List<FoodItem>
            {
                new FoodItem { Id = 1, Name = "Food 1", Description = "Description 1", Price = 10.0, PetId = 1, FoodTypeId = 1 },
                new FoodItem { Id = 2, Name = "Food 2", Description = "Description 2", Price = 15.0, PetId = 2, FoodTypeId = 1 },
                new FoodItem { Id = 3, Name = "Food 3", Description = "Description 3", Price = 12.5, PetId = 1, FoodTypeId = 2 },
            };

            var matchingFoodItems = foodItems.Where(fi => fi.PetId == petId && fi.FoodTypeId == foodTypeId);

            var foodItemsDto = matchingFoodItems.Select(fi => new FoodItemDto
            {
                Id = fi.Id,
                Name = fi.Name,
                Description = fi.Description,
                Price = fi.Price,
                PetId = fi.PetId,
                FoodTypeId = fi.FoodTypeId
            });

            _repositoryManagerMock.Setup(repo => repo.FoodItem.GetFoodItemByPetAndFoodType(petId, foodTypeId)).ReturnsAsync(matchingFoodItems);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<FoodItemDto>>(matchingFoodItems)).Returns(foodItemsDto);

            // Act
            var result = await _foodItemService.GetFoodItemsByPetAndFoodType(petId, foodTypeId);

            // Assert
            result.Should().BeEquivalentTo(foodItemsDto);
        }

        [Fact]
        public async Task GetFoodItemsByPet_ShouldReturnFoodItemsForPet()
        {
            // Arrange
            var petId = 1;

            var foodItems = new List<FoodItem>
            {
                new FoodItem { Id = 1, Name = "Food 1", Description = "Description 1", Price = 10.0, PetId = 1, FoodTypeId = 1 },
                new FoodItem { Id = 2, Name = "Food 2", Description = "Description 2", Price = 15.0, PetId = 2, FoodTypeId = 1 },
                new FoodItem { Id = 3, Name = "Food 3", Description = "Description 3", Price = 12.5, PetId = 1, FoodTypeId = 2 },
            };

            var petFoodItems = foodItems.Where(fi => fi.PetId == petId);

            var foodItemsDto = petFoodItems.Select(fi => new FoodItemDto
            {
                Id = fi.Id,
                Name = fi.Name,
                Description = fi.Description,
                Price = fi.Price,
                PetId = fi.PetId,
                FoodTypeId = fi.FoodTypeId
            });

            _repositoryManagerMock.Setup(repo => repo.FoodItem.GetFoodItemByPet(petId)).ReturnsAsync(petFoodItems);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<FoodItemDto>>(petFoodItems)).Returns(foodItemsDto);

            // Act
            var result = await _foodItemService.GetFoodItemsByPet(petId);

            // Assert
            result.Should().BeEquivalentTo(foodItemsDto);
        }

        [Fact]
        public async Task GetFoodItemsByFoodType_ShouldReturnFoodItemsForFoodType()
        {
            // Arrange
            var foodTypeId = 1;

            var foodItems = new List<FoodItem>
            {
                new FoodItem { Id = 1, Name = "Food 1", Description = "Description 1", Price = 10.0, PetId = 1, FoodTypeId = 1 },
                new FoodItem { Id = 2, Name = "Food 2", Description = "Description 2", Price = 15.0, PetId = 2, FoodTypeId = 1 },
                new FoodItem { Id = 3, Name = "Food 3", Description = "Description 3", Price = 12.5, PetId = 1, FoodTypeId = 2 },
            };

            var foodTypeFoodItems = foodItems.Where(fi => fi.FoodTypeId == foodTypeId);

            var foodItemsDto = foodTypeFoodItems.Select(fi => new FoodItemDto
            {
                Id = fi.Id,
                Name = fi.Name,
                Description = fi.Description,
                Price = fi.Price,
                PetId = fi.PetId,
                FoodTypeId = fi.FoodTypeId
            });

            _repositoryManagerMock.Setup(repo => repo.FoodItem.GetFoodItemByFoodType(foodTypeId)).ReturnsAsync(foodTypeFoodItems);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<FoodItemDto>>(foodTypeFoodItems)).Returns(foodItemsDto);

            // Act
            var result = await _foodItemService.GetFoodItemsByFoodType(foodTypeId);

            // Assert
            result.Should().BeEquivalentTo(foodItemsDto);
        }
    }
}
