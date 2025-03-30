using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;

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
        public async Task<ActionResult<OfferResponse>> Create([FromBody] CreateOfferRequest request)
        {
            var offer = await _offerService.CreateOfferAsync(request);

            var response = new OfferResponse
            {
                Id = offer.Id,
                ProductId = offer.ProductId,
                CategoryId = offer.CategoryId,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                IsArchived = offer.IsArchived,
                CategoryName = offer.Category?.Name,
                Quantity = offer.Quantity,
            };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferResponse>>> GetAll()
        {
            var offers = await _offerService.GetAllAsync();

            return Ok(offers.Select(o => new OfferResponse
            {
                Id = o.Id,
                ProductId = o.ProductId,
                CategoryId = o.CategoryId,
                StartDate = o.StartDate,
                EndDate = o.EndDate,
                Price = o.Price,
                IsArchived = o.IsArchived,
                CategoryName = o.Category?.Name,
                Quantity = o.Quantity,

            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferResponse>> GetById(int id)
        {
            var offer = await _offerService.GetByIdAsync(id);
            if (offer == null)
                return NotFound();

            return Ok(new OfferResponse
            {
                Id = offer.Id,
                ProductId = offer.ProductId,
                CategoryId = offer.CategoryId,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Price = offer.Price,
                IsArchived = offer.IsArchived,
                CategoryName = offer.Category?.Name,
                Quantity = offer.Quantity,
            });
        }
    }
}
