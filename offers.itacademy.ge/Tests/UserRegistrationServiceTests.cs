using FluentAssertions;
using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.services;
using ITAcademy.Offers.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ITAcademy.Offers.Application.Tests
{
    public class UserRegistrationServiceTests
    {
        private readonly Mock<UserManager<Client>> _userManagerMock;
        private readonly UserRegistrationService _registrationService;

        public UserRegistrationServiceTests()
        {
            _userManagerMock = new Mock<UserManager<Client>>(
                Mock.Of<IUserStore<Client>>(),
                null, null, null, null, null, null, null, null
            );

            _registrationService = new UserRegistrationService(_userManagerMock.Object);
        }

        [Fact]
        public async Task Registration_CreatesClient_ReturnsSuccessResult()
        {
            // Arrange
            var dto = new CreateClientDto
            {
                Email = "test@example.com",
                Password = "SecurePassword123!",
                UserType = UserType.Buyer,
                Buyer = new Buyer
                {
                    Name = "Test",
                    Surname = "User",
                    Address = "123 Test St",
                    Balance = 50
                }
            };

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<Client>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _registrationService.Registration(dto);

            // Assert
            result.Should().NotBeNull();
            result.IdentityResult.Succeeded.Should().BeTrue();
            result.Client.Email.Should().Be(dto.Email);
            result.Client.UserType.Should().Be(UserType.Buyer);
            result.Client.Buyer.Should().NotBeNull();
        }

        [Fact]
        public async Task RegisterBuyer_CreatesBuyerClient_ReturnsSuccessResult()
        {
            // Arrange
            var dto = new RegisterBuyerDto
            {
                Email = "buyer@example.com",
                Password = "Password123!",
                Name = "John",
                Surname = "Doe",
                Address = "456 Main Rd",
                Balance = 100
            };

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<Client>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _registrationService.RegisterBuyer(dto);

            // Assert
            result.Should().NotBeNull();
            result.IdentityResult.Succeeded.Should().BeTrue();
            result.Client.Email.Should().Be(dto.Email);
            result.Client.UserType.Should().Be(UserType.Buyer);
            result.Client.Buyer!.Name.Should().Be("John");
        }

        [Fact]
        public async Task RegisterCompany_CreatesCompanyClient_ReturnsSuccessResult()
        {
            // Arrange
            var dto = new RegisterCompanyDto
            {
                Email = "company@example.com",
                Password = "Password123!",
                Name = "My Co",
                Description = "Great company",
                PhotoUrl = "http://img.com/photo.png"
            };

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<Client>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _registrationService.RegisterCompany(dto);

            // Assert
            result.Should().NotBeNull();
            result.IdentityResult.Succeeded.Should().BeTrue();
            result.Client.Email.Should().Be(dto.Email);
            result.Client.UserType.Should().Be(UserType.Company);
            result.Client.Company!.Name.Should().Be("My Co");
            result.Client.Company!.IsActive.Should().BeFalse();
        }
    }
}
