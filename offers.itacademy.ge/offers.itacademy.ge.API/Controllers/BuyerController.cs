using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Domain.entities;
using System;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/buyer")]
    public class BuyerController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IBuyerService _buyerService;

        public BuyerController(IUserRegistrationService userRegistrationService, IBuyerService buyerService)
        {
            _userRegistrationService = userRegistrationService;
            _buyerService = buyerService;
        }


        [HttpPost("{id}/add-money")]
        public async Task<IActionResult> AddMoney(int id, [FromBody] AddMoneyRequest request, CancellationToken cancellationToken)
        {
            if (await _buyerService.AddMoneyToBuyer(id, request.Amount, cancellationToken))
            {
                return Ok("Money added successfully.");

            }
            return BadRequest("Money could not be added.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterBuyerRequest request)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var buyer = await _buyerService.GetBuyerById(id, cancellationToken);
            if (buyer == null) return NotFound();
            return Ok(new BuyerResponse
            {
                Id = buyer.Id,
                Address = buyer.Address,
                Balance = buyer.Balance,
                CreatedAt = buyer.CreatedAt,
                Name = buyer.Name,
                PhotoUrl = buyer.PhotoUrl,
                Surname = buyer.Surname,
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var buyers = await _buyerService.GetAllBuyers(cancellationToken);
            var response = buyers.Select(buyer => new BuyerResponse
            {
                Id = buyer.Id,
                Address = buyer.Address,
                Balance = buyer.Balance,
                CreatedAt = buyer.CreatedAt,
                Name = buyer.Name,
                PhotoUrl = buyer.PhotoUrl,
                Surname = buyer.Surname,
            });
            return Ok(response);
        }
    }
}
