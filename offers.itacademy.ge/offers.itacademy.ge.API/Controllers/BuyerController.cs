using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Extentions.offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Security.Claims;

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

        [Authorize(Policy = "MustBuyer")]
        [HttpPost("add-money")]
        public async Task<IActionResult> AddMoney( [FromBody] AddMoneyRequest request, CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();
          
            if (await _buyerService.AddMoneyToBuyer(buyerId, request.Amount, cancellationToken))
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
        [Authorize(Policy = "MustBuyer")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe( CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();
            var buyer = await _buyerService.GetBuyerById(buyerId, cancellationToken);
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
        [Authorize(Policy = "MustAdmin")]
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

        [Authorize(Policy = "MustBuyer")]
        [HttpGet("MySubscribedOffers")]
        public async Task<IActionResult> GetSubscribedOffers( CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();
            var offers = await _offerService.GetSubscribedOffers(buyerId, cancellationToken);

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

        [HttpPost("upload-Image")]
        [Authorize(Policy = "MustBuyer")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage( IFormFile file)
        {
            var buyerId = User.GetBuyerId();
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();
            var base64 = Convert.ToBase64String(bytes);

            await _buyerService.UploadImage(base64, buyerId);

            return Ok(new { message = "Image uploaded successfully." });

        }
    }
}
