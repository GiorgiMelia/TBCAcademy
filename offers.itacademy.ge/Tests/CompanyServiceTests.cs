using FluentAssertions;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.services;
using ITAcademy.Offers.Domain.Entities;
using ITAcademy.Offers.Persistence.Data;
using ITAcademy.Offers.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITAcademy.Offers.Application.Tests
{
    public class CompanyServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CompanyRepository _companyRepository;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _companyRepository = new CompanyRepository(_context);
            _companyService = new CompanyService(_companyRepository);
        }

        [Fact]
        public async Task ActivateCompany_WhenCompanyExists_ActivatesSuccessfully()
        {
            var company = new Company { Name = "InactiveCo", IsActive = false, Description = "asda" };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            await _companyService.ActivateCompany(company.Id!.Value, CancellationToken.None);

            var updated = await _context.Companies.FindAsync(company.Id);
            updated!.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task ActivateCompany_WhenCompanyNotFound_ThrowsNotFoundException()
        {
            Func<Task> act = async () => await _companyService.ActivateCompany(999, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Company not found");
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsAllCompanies()
        {
            _context.Companies.AddRange(
                new Company { Name = "A", Description = "asda" },
                new Company { Name = "B", Description = "asda" }
            );
            await _context.SaveChangesAsync();

            var result = await _companyService.GetAllCompanies(CancellationToken.None);

            result.Should().HaveCount(2);
        }

        [Theory]
        [InlineData("AlphaCo")]
        [InlineData("BetaInc")]
        public async Task GetCompanyById_WhenExists_ReturnsCompany(string companyName)
        {
            var company = new Company { Name = companyName, Description = "asda" };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var result = await _companyService.GetCompanyById(company.Id!.Value, CancellationToken.None);

            result.Should().NotBeNull();
            result!.Name.Should().Be(companyName);
        }

        [Fact]
        public async Task GetCompanyById_WhenNotExists_ReturnsNull()
        {
            var result = await _companyService.GetCompanyById(999, CancellationToken.None);
            result.Should().BeNull();
        }

        [Theory]
        [InlineData("data:image/jpeg;base64,AAA123")]
        [InlineData("http://cdn.images/test.jpg")]
        public async Task UploadImage_UpdatesPhotoUrl(string photo)
        {
            var company = new Company { Name = "PhotoCorp", Description = "asda" };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            await _companyService.UploadImage(photo, company.Id!.Value);

            var result = await _context.Companies.FindAsync(company.Id);
            result!.PhotoUrl.Should().Be(photo);
        }
    }
}
