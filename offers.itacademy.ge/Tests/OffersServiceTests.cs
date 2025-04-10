using FluentAssertions;
using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Application.services;
using ITAcademy.Offers.Domain.Entities;
using ITAcademy.Offers.Persistence.Data;
using ITAcademy.Offers.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ITAcademy.Offers.Application.Tests
{
    public class OfferServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly OfferRepository _offerRepository;
        private readonly Mock<IPurchaseService> _purchaseServiceMock;
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly OfferService _offerService;

        public OfferServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _offerRepository = new OfferRepository(_context);
            _purchaseServiceMock = new Mock<IPurchaseService>();
            _companyServiceMock = new Mock<ICompanyService>();

            _offerService = new OfferService(_offerRepository, _purchaseServiceMock.Object, _companyServiceMock.Object);
        }

        [Fact]
        public async Task CreateOffer_WhenCompanyExistsAndActive_ReturnsOffer()
        {
            var dto = new OfferDto
            {
                CompanyId = 1,
                ProductName = "Product",
                ProductDescription = "Description",
                CategoryId = 1,
                Price = 10,
                Quantity = 5,
                EndDate = DateTime.UtcNow.AddHours(1)
            };

            _companyServiceMock
                .Setup(c => c.GetCompanyById(dto.CompanyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Company { Id = 1, IsActive = true });

            var result = await _offerService.CreateOffer(dto, CancellationToken.None);

            result.Should().NotBeNull();
            result.CompanyId.Should().Be(1);
        }

        [Fact]
        public async Task CreateOffer_WhenCompanyDoesNotExist_ThrowsNotFoundException()
        {
            var dto = new OfferDto { CompanyId = 99, EndDate = DateTime.UtcNow.AddHours(1) };

            _companyServiceMock
                .Setup(c => c.GetCompanyById(99, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Company?)null);

            Func<Task> act = async () => await _offerService.CreateOffer(dto, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Company with99 does not exist");
        }

        [Fact]
        public async Task CreateOffer_WhenCompanyIsInactive_ThrowsWrongRequestException()
        {
            var dto = new OfferDto { CompanyId = 1, EndDate = DateTime.UtcNow.AddHours(1) };

            _companyServiceMock
                .Setup(c => c.GetCompanyById(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Company { Id = 1, IsActive = false });

            Func<Task> act = async () => await _offerService.CreateOffer(dto, CancellationToken.None);

            await act.Should().ThrowAsync<WrongRequestException>()
                .WithMessage("Company is not Active!");
        }

        [Fact]
        public async Task GetAllOffers_Always_ReturnsOfferList()
        {
            _context.Offers.AddRange(
                new Offer { CompanyId = 1, ProductName = "One", ProductDescription = "desc" },
                new Offer { CompanyId = 2, ProductName = "Two", ProductDescription = "desc" }
            );
            await _context.SaveChangesAsync();

            var result = await _offerService.GetAllOffers(CancellationToken.None);

            result.Should().HaveCount(2);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(999, false)]
        public async Task GetOfferById_ReturnsExpectedResult(int id, bool shouldExist)
        {
            if (shouldExist)
            {
                _context.Offers.Add(new Offer { Id = id, CompanyId = 1, ProductName = "Found", ProductDescription = "desc" });
                await _context.SaveChangesAsync();
            }

            var result = await _offerService.GetOfferById(id, CancellationToken.None);

            if (shouldExist)
                result.Should().NotBeNull();
            else
                result.Should().BeNull();
        }

        [Fact]
        public async Task CancelOffer_WhenValidAndWithin10Minutes_ReturnsTrue()
        {
            var offer = new Offer
            {
                CompanyId = 1,
                ProductName = "bla",
                ProductDescription = "desc",
                StartDate = DateTime.UtcNow.AddMinutes(-5),
                EndDate = DateTime.UtcNow.AddMinutes(30),
                IsArchived = false,
                IsCanceled = false
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            _purchaseServiceMock
                .Setup(p => p.CancelPurchaseByOffer(offer.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await _offerService.CancelOffer(offer.Id, 1, CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CancelOffer_WhenOfferNotFound_ReturnsFalse()
        {
            var result = await _offerService.CancelOffer(999, 1, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CancelOffer_WhenOfferOlderThan10Minutes_ReturnsFalse()
        {
            var offer = new Offer
            {
                CompanyId = 1,
                ProductName = "Test",
                ProductDescription = "Test decr",
                Price = 20,
                StartDate = DateTime.UtcNow.AddMinutes(-15),
                EndDate = DateTime.UtcNow.AddMinutes(30),
                IsArchived = false,
                IsCanceled = false
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            var result = await _offerService.CancelOffer(offer.Id, 1, CancellationToken.None);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public async Task CancelOffer_WhenOfferIsArchivedOrCanceled_ReturnsFalse(bool isArchived, bool isCanceled)
        {
            var offer = new Offer
            {
                CompanyId = 1,
                ProductName = "Test",
                ProductDescription = "Test decr",
                IsArchived = isArchived,
                IsCanceled = isCanceled,
                StartDate = DateTime.UtcNow.AddMinutes(-5),
                EndDate = DateTime.UtcNow.AddHours(1)
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            var result = await _offerService.CancelOffer(offer.Id, 1, CancellationToken.None);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ArchiveOldOffers_ArchivesExpiredOffers()
        {
            var expired = new Offer
            {
                ProductName = "Test",
                ProductDescription = "Test decr",
                EndDate = DateTime.UtcNow.AddMinutes(-1),
                IsArchived = false
            };

            var active = new Offer
            {
                ProductName = "Test2",
                ProductDescription = "Test decr2",
                EndDate = DateTime.UtcNow.AddHours(1),
                IsArchived = false
            };

            _context.Offers.AddRange(expired, active);
            await _context.SaveChangesAsync();

            await _offerService.ArchiveOldOffers(CancellationToken.None);

            (await _context.Offers.FindAsync(expired.Id))!.IsArchived.Should().BeTrue();
            (await _context.Offers.FindAsync(active.Id))!.IsArchived.Should().BeFalse();
        }

        [Fact]
        public async Task GetOffersByCompany_ReturnsOnlyThatCompanysOffers()
        {
            _context.Offers.AddRange(
                new Offer { CompanyId = 1, ProductName = "A", ProductDescription = "desc" },
                new Offer { CompanyId = 2, ProductName = "B", ProductDescription = "desc" },
                new Offer { CompanyId = 1, ProductName = "C", ProductDescription = "desc" }
            );
            await _context.SaveChangesAsync();

            var result = await _offerService.GetOffersByCompany(1, CancellationToken.None);

            result.Should().OnlyContain(o => o.CompanyId == 1);
        }

        [Fact]
        public async Task GetSubscribedOffers_ReturnsOffersMatchingBuyerSubscriptions()
        {
            var buyerId = 99;

            _context.Subscriptions.AddRange(
                new Subscription { BuyerId = buyerId, CategoryId = 1 },
                new Subscription { BuyerId = buyerId, CategoryId = 2 }
            );

            _context.Offers.AddRange(
                new Offer { CategoryId = 1, IsArchived = false, IsCanceled = false, ProductName = "A", ProductDescription = "desc" },
                new Offer { CategoryId = 2, IsArchived = false, IsCanceled = false, ProductName = "B", ProductDescription = "desc" },
                new Offer { CategoryId = 3, IsArchived = false, IsCanceled = false, ProductName = "C", ProductDescription = "desc" }, // not subscribed
                new Offer { CategoryId = 1, IsArchived = true, IsCanceled = false, ProductName = "D", ProductDescription = "desc" },  // archived
                new Offer { CategoryId = 2, IsArchived = false, IsCanceled = true, ProductName = "E", ProductDescription = "desc" }   // canceled
            );

            await _context.SaveChangesAsync();

            var result = await _offerService.GetSubscribedOffers(buyerId, CancellationToken.None);

            result.Should().HaveCount(2);
        }
    }
}
