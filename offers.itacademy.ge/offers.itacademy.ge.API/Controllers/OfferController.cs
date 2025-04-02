using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Dtos;
using System;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost]
        public async Task<ActionResult<OfferResponse>> Create([FromBody] OfferRequest request)
        {
            var OfferDto = new OfferDto
            {
                CategoryId = request.CategoryId,
                EndDate = request.EndDate,
                Price = request.Price,
                ProductDescription = request.ProductDescription,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
            };
            var offer = await _offerService.CreateOffer(OfferDto);

            var response = new OfferResponse
            {
                Id = offer.Id,
                ProductDescription = offer.ProductDescription,
                ProductName = offer.ProductName,
                CategoryId = request.CategoryId,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                Quantity = offer.Quantity,
            };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferResponse>>> GetAll()
        {
            var offers = await _offerService.GetAllOffers();

            return Ok(offers.Select(offer => new OfferResponse
            {
                Id = offer.Id,
                ProductDescription = offer.ProductDescription,
                ProductName = offer.ProductName,
                CategoryId = offer.CategoryId,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                Quantity = offer.Quantity,
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferResponse>> GetById(int id)
        {
            var offer = await _offerService.GetOfferById(id);
            if (offer == null)
                return NotFound();

            return Ok(new OfferResponse
            {
                Id = offer.Id,
                ProductDescription = offer.ProductDescription,
                ProductName = offer.ProductName,
                CategoryId = offer.CategoryId,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                Quantity = offer.Quantity,
            });
        }
    }
}
