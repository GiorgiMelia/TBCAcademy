using FluentAssertions;
using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.services;
using ITAcademy.Offers.Domain.Entities;
using ITAcademy.Offers.Persistence.Data;
using ITAcademy.Offers.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITAcademy.Offers.Application.Tests
{
    public class SubscriptionServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _subscriptionRepository = new SubscriptionRepository(_context);
            _subscriptionService = new SubscriptionService(_subscriptionRepository);
        }

        [Fact]
        public async Task CreateSubscription_WhenValid_ReturnsSubscription()
        {
            // Arrange
            var dto = new SubscriptionDto { BuyerId = 1, CategoryId = 10 };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _subscriptionService.CreateSubscription(dto, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.BuyerId.Should().Be(1);
            result.CategoryId.Should().Be(10);

            var exists = await _context.Subscriptions.AnyAsync(s => s.BuyerId == 1 && s.CategoryId == 10);
            exists.Should().BeTrue();
        }


        [Fact]
        public async Task CreateSubscription_WhenAlreadyExists_ThrowsWrongRequestException()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            _context.Subscriptions.Add(new Subscription { BuyerId = 1, CategoryId = 10 });
            await _context.SaveChangesAsync(cancellationToken);

            var dto = new SubscriptionDto { BuyerId = 1, CategoryId = 10 };

            // Act
            Func<Task> act = async () => await _subscriptionService.CreateSubscription(dto, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<WrongRequestException>()
                .WithMessage("Category already subscribed!");
        }


        [Fact]
        public async Task GetAllSubscriptions_Always_ReturnsList()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _context.Subscriptions.AddRange(
                new Subscription { BuyerId = 1, CategoryId = 10 },
                new Subscription { BuyerId = 2, CategoryId = 20 }
            );
            await _context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await _subscriptionService.GetAllSubscriptions(cancellationToken);

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainSingle(s => s.BuyerId == 1 && s.CategoryId == 10);
            result.Should().ContainSingle(s => s.BuyerId == 2 && s.CategoryId == 20);
        }

        [Fact]
        public async Task GetSubscriptionById_WhenExists_ReturnsSubscription()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var subscription = new Subscription { BuyerId = 1, CategoryId = 99 };
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await _subscriptionService.GetSubscriptionById(subscription.Id, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(subscription.Id);
            result.BuyerId.Should().Be(1);
            result.CategoryId.Should().Be(99);
        }

        [Fact]
        public async Task GetSubscriptionById_WhenNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            Func<Task> act = async () => await _subscriptionService.GetSubscriptionById(12345, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Subscription not found");
        }


        [Fact]
        public async Task DeleteSubscription_WhenFound_ReturnsTrue()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var sub = new Subscription { BuyerId = 1, CategoryId = 5 };
            _context.Subscriptions.Add(sub);
            await _context.SaveChangesAsync(cancellationToken);

            // Act
            var result = await _subscriptionService.DeleteSubscription(sub.Id, sub.BuyerId, cancellationToken);

            // Assert
            result.Should().BeTrue();
            _context.Subscriptions.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteSubscription_WhenNotFound_ReturnsFalse()
        {
            // Act
            var result = await _subscriptionService.DeleteSubscription(999, 1, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }

    }
}
