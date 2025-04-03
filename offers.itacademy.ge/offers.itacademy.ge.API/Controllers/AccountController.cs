using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public AccountController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }

        [HttpPost("register/buyer")]
        public async Task<IActionResult> RegisterBuyer([FromBody] RegisterBuyerRequest request)
        {
            var dto = new RegisterBuyerDto
            {
                Address = request.Address,
                Balance = request.Balance,
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Surname = request.Surname,
            };
            var result = await _userRegistrationService.RegisterBuyer(dto);
            if (!result.IdentityResult.Succeeded)
                return BadRequest(result.IdentityResult.Errors);

            return Ok(new RegisterBuyerResponse
            {
                Id = result.Client.Id,
                Email = result.Client.Email
            });
        }

        [HttpPost("register/company")]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyRequest request)
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
