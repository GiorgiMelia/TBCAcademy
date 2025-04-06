using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly ICompanyService _companyService;

        public CompanyController(IUserRegistrationService userRegistrationService, ICompanyService companyService)
        {
            _userRegistrationService = userRegistrationService;
            _companyService = companyService;
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
        public async Task<IActionResult> Activate(int companyId, CancellationToken cancellationToken)
        {
            await _companyService.ActivateCompany(companyId, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var company = await _companyService.GetCompanyById(id, cancellationToken);
            if (company == null) return NotFound();
            return Ok(new CompanyResponse
            {
                Description=company.Description,
                IsActive=company.IsActive,
                Name=company.Name,
                ImageUrl = company.PhotoUrl,
                CreatedAt = company.CreatedAt,
                Id = company.Id,

            });
        }
        [HttpGet]
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
    }
}
