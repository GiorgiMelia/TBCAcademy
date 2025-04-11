using FluentAssertions;
using Moq;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Exceptions;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Infrastructure.Repositories;


namespace Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object); 
        }

        [Fact]
        public async Task CreateCategory_WhenCategoryNotExists_ReturnsCategory()
        {
            // Arrange
            var dto = new CategoryDto { CategoryName = "Snacks" };

            _categoryRepositoryMock
                .Setup(r => r.CategoryExists("Snacks"))
                .ReturnsAsync(false);

            _categoryRepositoryMock
                .Setup(r => r.CreateCategory(It.IsAny<Category>()))
                .ReturnsAsync((Category c) => c);

            // Act
            var result = await _categoryService.CreateCategory(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Snacks");

            _categoryRepositoryMock.Verify(r =>
                r.CategoryExists("Snacks"), Times.Once);

            _categoryRepositoryMock.Verify(r =>
                r.CreateCategory(It.Is<Category>(c => c.Name == "Snacks")), Times.Once);

        }
        [Fact]
        public async Task CreateCategory_WhenCategoryExists_ThrowsWrongRequestException()
        {
            // Arrange
            var dto = new CategoryDto { CategoryName = "Drinks" };

            _categoryRepositoryMock
                .Setup(r => r.CategoryExists("Drinks"))
                .ReturnsAsync(true);

            // Act
            Func<Task> act = async () => await _categoryService.CreateCategory(dto);

            // Assert
            await act.Should().ThrowAsync<WrongRequestException>()
                .WithMessage("Category already exists");

            _categoryRepositoryMock.Verify(r =>
                r.CategoryExists("Drinks"), Times.Once);

            _categoryRepositoryMock.Verify(r =>
                r.CreateCategory(It.IsAny<Category>()), Times.Never);

        }
        [Fact]
        public async Task GetAllCategories_Always_ReturnsCategoryList()
        {
            // Arrange
            var expected = new List<Category>
    {
        new Category { Id = 1, Name = "Food" },
        new Category { Id = 2, Name = "Drinks" }
    };

            _categoryRepositoryMock
                .Setup(r => r.GetAllCategories())
                .ReturnsAsync(expected);

            // Act
            var result = await _categoryService.GetAllCategories();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task GetCategoryById_WhenCategoryExists_ReturnsCategory()
        {
            // Arrange
            var expected = new Category { Id = 1, Name = "Snacks" };

            _categoryRepositoryMock
                .Setup(r => r.GetCategoryById(1))
                .ReturnsAsync(expected);

            // Act
            var result = await _categoryService.GetCategoryById(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.Name.Should().Be("Snacks");
        }
        [Fact]
        public async Task GetCategoryById_WhenCategoryNotExists_ReturnsCategory()
        {
            // Arrange
            _categoryRepositoryMock
                .Setup(r => r.GetCategoryById(99)) // random non-existent id
                .ReturnsAsync((Category?)null);

            // Act
            var result = await _categoryService.GetCategoryById(99);

            // Assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task UpdateCategory_WhenCategoryExists_updatesCategory()
        {
            // Arrange
            var existing = new Category { Id = 1, Name = "OldName" };
            var dto = new CategoryDto { CategoryName = "UpdatedName" };

            _categoryRepositoryMock
                .Setup(r => r.GetCategoryById(1))
                .ReturnsAsync(existing);

            _categoryRepositoryMock
                .Setup(r => r.UpdateCategory(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryService.UpdateCategory(1, dto);

            // Assert
            result.Should().BeTrue();
            existing.Name.Should().Be("UpdatedName");

            _categoryRepositoryMock.Verify(r =>
                r.UpdateCategory(It.Is<Category>(c => c.Id == 1 && c.Name == "UpdatedName")), Times.Once);
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryNotExists_ReturnsFalse()
        {
            // Arrange
            var dto = new CategoryDto { CategoryName = "DoesNotMatter" };

            _categoryRepositoryMock
                .Setup(r => r.GetCategoryById(999)) // non-existent ID
                .ReturnsAsync((Category?)null);

            // Act
            var result = await _categoryService.UpdateCategory(999, dto);

            // Assert
            result.Should().BeFalse();

            _categoryRepositoryMock.Verify(r => r.UpdateCategory(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCategory_WhenCategoryExists_ReturnsTrue()
        {
            // Arrange
            _categoryRepositoryMock
                .Setup(r => r.DeleteCategory(1))
                .ReturnsAsync(true);

            // Act
            var result = await _categoryService.DeleteCategory(1);

            // Assert
            result.Should().BeTrue();

            _categoryRepositoryMock.Verify(r => r.DeleteCategory(1), Times.Once);
        }
        [Fact]
        public async Task DeleteCategory_WhenCategoryNotExists_ReturnsFalse()
        {
            // Arrange
            _categoryRepositoryMock
                .Setup(r => r.DeleteCategory(999))
                .ReturnsAsync(false);

            // Act
            var result = await _categoryService.DeleteCategory(999);

            // Assert
            result.Should().BeFalse();

            _categoryRepositoryMock.Verify(r => r.DeleteCategory(999), Times.Once);
        }
        [Fact]
        public async Task DeleteCategory_WhenCategoryHasOffers_ThrowsInvalidOperationException()
        {
            // Arrange
            _categoryRepositoryMock
                .Setup(r => r.DeleteCategory(1))
                .ThrowsAsync(new WrongRequestException("Cannot delete category with associated offers."));

            // Act
            Func<Task> act = async () => await _categoryService.DeleteCategory(1);

            // Assert
            await act.Should().ThrowAsync<WrongRequestException>()
                .WithMessage("Cannot delete category with associated offers.");

            _categoryRepositoryMock.Verify(r => r.DeleteCategory(1), Times.Once);
        }



    }
}
