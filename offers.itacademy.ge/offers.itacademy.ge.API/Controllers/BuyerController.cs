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
        private readonly IOfferService _offerService;

        public BuyerController(IUserRegistrationService userRegistrationService, IBuyerService buyerService, IOfferService offerService)
        {
            _userRegistrationService = userRegistrationService;
            _buyerService = buyerService;
            _offerService = offerService;
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
        [HttpGet("{id}/subscribedOffers")]
        public async Task<IActionResult> GetSubscribedOffers(int id, CancellationToken cancellationToken)
        {
            var offers = await _offerService.GetSubscribedOffers(id, cancellationToken);

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
                IsCanceled = offer.IsCanceled
            }));

        }
    }
}
