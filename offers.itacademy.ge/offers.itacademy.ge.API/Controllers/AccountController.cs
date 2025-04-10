using ITAcademy.Offers.API.Models;
using ITAcademy.Offers.API.Tokens;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.Offers.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly UserManager<Client> _userManager;
        private readonly IJWTTokenService _jWTTokenService;
        private readonly SignInManager<Client> _signInManager;


        public AccountController(IUserRegistrationService userRegistrationService, SignInManager<Client> signInManager, UserManager<Client> userManager, IJWTTokenService jWTTokenService)
        {
            _userRegistrationService = userRegistrationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTTokenService = jWTTokenService;
        }


        [Route("login")]
        [HttpPost]
        public async Task<string> LogIn(UserLoginRequest user, CancellationToken cancellation)
        {
            var userr = await _userManager.FindByEmailAsync(user.Username);
            if (userr == null) throw new WrongRequestException("Wrong Login attempt");

            var result = await _signInManager.CheckPasswordSignInAsync(userr, user.Password, false);
            var roles = await _userManager.GetRolesAsync(userr);
            if (result.Succeeded)
            {
                var token = _jWTTokenService.GenerateToken(userr, roles);
                return token;

            }
            else throw new WrongRequestException("Wrong Login attempt");
        }
    }
}
