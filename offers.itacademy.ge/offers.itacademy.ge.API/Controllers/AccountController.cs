﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.API.Tokens;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Exceptions;
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
                var token =  _jWTTokenService.GenerateToken(userr,roles);
                return token;

            }
            else throw new WrongRequestException("Wrong Login attempt");
        }
    }
}
