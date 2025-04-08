using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Extentions.offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly ICompanyService _companyService;
        private readonly IOfferService _offerService;
        public CompanyController(IUserRegistrationService userRegistrationService, ICompanyService companyService, IOfferService offerService)
        {
            _userRegistrationService = userRegistrationService;
            _companyService = companyService;
            _offerService = offerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCompanyRequest request)
        {
            var dto = new RegisterCompanyDto
            {
                Description = request.Description,
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
            };

            var result = await _userRegistrationService.RegisterCompany(dto);
            if (!result.IdentityResult.Succeeded)
                return BadRequest(result.IdentityResult.Errors);

            return Ok(new RegisterCompanyResponse
            {
                Id = result.Client.Id,
                Email = result.Client.Email
            });
        }
        [HttpPut("{companyId}/activate")]
        [Authorize(Policy = "MustAdmin")]
        public async Task<IActionResult> Activate(int companyId, CancellationToken cancellationToken)
        {
            await _companyService.ActivateCompany(companyId, cancellationToken);
            return NoContent();
        }

        [Authorize(Policy = "MustCompany")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
        {
            var companyId = User.GetCompanyId();
            var company = await _companyService.GetCompanyById(companyId, cancellationToken);
            if (company == null) return NotFound();

            return Ok(new CompanyResponse
            {
                Description = company.Description,
                IsActive = company.IsActive,
                Name = company.Name,
                ImageUrl = company.PhotoUrl,
                CreatedAt = company.CreatedAt,
                Id = company.Id,

            });
        }
        [HttpGet]
        [Authorize(Policy = "MustAdmin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var companies = await _companyService.GetAllCompanies(cancellationToken);
            var response = companies.Select(company => new CompanyResponse
            {
                Description = company.Description,
                IsActive = company.IsActive,
                Name = company.Name,
                ImageUrl = company.PhotoUrl,
                CreatedAt = company.CreatedAt,
                Id = company.Id,

            });
            return Ok(response);
        }
        [HttpGet("{companyId}/GetOffers")]
        public async Task<IActionResult> GetOffersByCompany(int companyId, CancellationToken cancellationToken)
        {

            var offers = await _offerService.GetOffersByCompany(companyId, cancellationToken);
            return Ok(offers.Select(offer => new OfferResponse
            {
                Id = offer.Id,
                ProductDescription = offer.ProductDescription,
                ProductName = offer.ProductName,
                CategoryId = offer.CategoryId,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                IsArchived = offer.IsArchived,
                Quantity = offer.Quantity,
                CompanyId = offer.CompanyId,
                IsCanceled = offer.IsCanceled,

            }));
        }
    }
}
