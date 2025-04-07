using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.API.Tokens;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly UserManager<Client> _userManager;
        private readonly IJWTTokenService _jWTTokenService;
        private readonly SignInManager<Client> _signInManager;


        public AccountController(IUserRegistrationService userRegistrationService, SignInManager<Client> signInManager, UserManager<Client> userManager)
        {
            _userRegistrationService = userRegistrationService;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [Route("login")]
        [HttpPost]
        //UserLogInRequest
        public async Task<string> LogIn(UserLoginRequest user, CancellationToken cancellation)
        {
            var userr = await _userManager.FindByEmailAsync(user.Username);
            if (userr == null) throw new BadHttpRequestException("Wrong Login attempt");

            var result = await _signInManager.CheckPasswordSignInAsync(userr, user.Password, false);
            
            if (result.Succeeded)
            {
                var token = await _jWTTokenService.GenerateToken(userr);
                return token;

            }
            else throw new BadHttpRequestException("Wrong Login attempt");
        }
    }
}
