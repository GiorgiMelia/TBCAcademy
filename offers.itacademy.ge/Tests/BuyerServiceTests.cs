using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using offers.itacademy.ge.Application.Exceptions;
using ITAcademy.Offers.Persistence.Data;

namespace Tests
{
    public class BuyerServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly BuyerRepository _buyerRepository;
        private readonly BuyerService _buyerService;

        public BuyerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);
            _buyerRepository = new BuyerRepository(_context);
            _buyerService = new BuyerService(_buyerRepository);
        }

        [Fact]
        public async Task AddMoneyToBuyer_WhenBuyerExists_AddsAmountAndReturnsTrue()
        {
            // Arrange
            var buyer = new Buyer { Id = 1, Address = "nucub", Name = "saxeli", Surname = "gvari", Balance = 100, CreatedAt = DateTime.UtcNow };
            _context.Buyers.Add(buyer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _buyerService.AddMoneyToBuyer((int)buyer.Id, 50, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            var updated = await _context.Buyers.FindAsync(buyer.Id);
            updated!.Balance.Should().Be(150);
        }

        [Fact]
        public async Task AddMoneyToBuyer_WhenBuyerNotFound_ThrowsNotFoundException()
        {
            // Act
            Func<Task> act = async () =>
                await _buyerService.AddMoneyToBuyer(999, 50, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Buyer id not found!");
        }

        [Fact]
        public async Task GetAllBuyers_ReturnsAllBuyers()
        {
            // Arrange
            _context.Buyers.AddRange(
                new Buyer {Id = 2, Address = "nucub", Name = "saxeli", Surname = "gvari", Balance = 1, CreatedAt = DateTime.UtcNow },
                new Buyer {Id = 3, Address = "nucub", Name = "saxeli", Surname = "gvari", Balance = 1, CreatedAt = DateTime.UtcNow }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _buyerService.GetAllBuyers(CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBuyerById_WhenExists_ReturnsBuyer()
        {
            // Arrange
            var buyer = new Buyer {Id = 4, Address = "nucub", Name = "Target", Surname = "gvari", Balance = 1, CreatedAt = DateTime.UtcNow };
            _context.Buyers.Add(buyer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _buyerService.GetBuyerById((int)buyer.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Target");
        }

        [Fact]
        public async Task GetBuyerById_WhenNotExists_ReturnsNull()
        {
            // Act
            var result = await _buyerService.GetBuyerById(999, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateBuyer_ChangesDataInDatabase()
        {
            // Arrange
            var buyer = new Buyer { Id = 5, Address = "nucub", Name = "saxeli", Surname = "gvari", Balance = 1, CreatedAt = DateTime.UtcNow };
            _context.Buyers.Add(buyer);
            await _context.SaveChangesAsync();

            buyer.Name = "After";

            // Act
            await _buyerService.UpdateBuyer(buyer, CancellationToken.None);

            // Assert
            var updated = await _context.Buyers.FindAsync(buyer.Id);
            updated!.Name.Should().Be("After");
        }

        [Fact]
        public async Task UploadImage_UpdatesPhotoUrl()
        {
            // Arrange
            var buyer = new Buyer { Id =6,Address = "nucub",Name = "saxeli", Surname = "gvari", Balance=1,CreatedAt =DateTime.UtcNow };
            _context.Buyers.Add(buyer);
            await _context.SaveChangesAsync();

            var base64 = "data:image/png;base64,AAA...";

            // Act
            await _buyerService.UploadImage(base64,(int) buyer.Id);

            // Assert
            var updated = await _context.Buyers.FindAsync(buyer.Id);
            updated!.PhotoUrl.Should().Be(base64);
        }
    }
}
