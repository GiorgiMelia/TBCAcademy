using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Dtos;
using System;
using offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.API.Extentions.offers.itacademy.ge.API.Extentions;
using Microsoft.AspNetCore.Authorization;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ICompanyService _companyService;

        public OfferController(IOfferService offerService, ICompanyService companyService)
        {
            _offerService = offerService;
            _companyService = companyService;
        }

        [Authorize(Policy = "MustCompany")]
        [HttpPost]
        public async Task<ActionResult<OfferResponse>> Create([FromBody] OfferRequest request, CancellationToken cancellationToken)
        {
            var companyId = User.GetCompanyId();
            var company = await _companyService.GetCompanyById(companyId, cancellationToken);
            if (company == null) return NotFound();

            var OfferDto = new OfferDto
            {
                CompanyId = companyId,
                CategoryId = request.CategoryId,
                EndDate = request.EndDate,
                Price = request.Price,
                ProductDescription = request.ProductDescription,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
            };
            var offer = await _offerService.CreateOffer(OfferDto, cancellationToken);

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
                CompanyId = companyId,
                IsArchived = offer.IsArchived,
                IsCanceled = offer.IsCanceled,
            };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var offers = await _offerService.GetAllOffers( cancellationToken);

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
                IsCanceled = offer.IsCanceled,

            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferResponse>> GetById(int id, CancellationToken cancellationToken)
        {
            var offer = await _offerService.GetOfferById(id, cancellationToken);
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
                IsArchived = offer.IsArchived,
                Quantity = offer.Quantity,
                CompanyId = offer.CompanyId,
                IsCanceled = offer.IsCanceled,

            });
        }
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOffer(int id, CancellationToken cancellationToken)
        {
            if (!await _offerService.CancelOffer(id, cancellationToken))
                return BadRequest("Offer cannot be canceled (expired, already canceled, or not found).");
            return Ok("Offer canceled successfully.");
        }
    }
}
