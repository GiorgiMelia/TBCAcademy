using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public CompanyController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
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
    }
}
