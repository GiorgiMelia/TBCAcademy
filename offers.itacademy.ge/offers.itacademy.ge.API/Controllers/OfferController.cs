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
        public async Task<ActionResult<OfferResponse>> Create([FromBody] OfferDto request)
        {
            var offer = await _offerService.CreateOffer(request);

            var response = new OfferResponse
            {
                Id = offer.Id,
                ProductDescription = offer.ProductDescription,
                ProductName = offer.ProductName,
                CategoryName = offer.Category.Name,
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
                CategoryName = offer.Category.Name,
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
                CategoryName = offer.Category.Name,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                Quantity = offer.Quantity,
            });
        }
    }
}
