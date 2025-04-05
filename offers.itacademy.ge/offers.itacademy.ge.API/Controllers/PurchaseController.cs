using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseResponse>> Create([FromBody] PurchaseRequest request, CancellationToken cancellationToken)
        {
            var purchasedto = new PurchaseDto
            {
                BuyerId = request.BuyerId,
                OfferId = request.OfferId,
                Quantity = request.Quantity,
            };
            var purchase = await _purchaseService.CreatePurchase(purchasedto,cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.Id,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                TotalPayment = purchase.Quantity * purchase.Offer.Price,
                IsCanceled = purchase.IsCanceled
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var purchases = await _purchaseService.GetAllPurchases(cancellationToken);

            return Ok(purchases.Select(purchase => new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.Id,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                TotalPayment = purchase.Quantity * purchase.Offer.Price,
                IsCanceled = purchase.IsCanceled
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseResponse>> GetById(int id, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseService.GetPurchaseById(id,cancellationToken);
            if (purchase == null)
                return NotFound();

            return Ok(new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.Id,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                TotalPayment = purchase.Quantity * purchase.Offer.Price,
                IsCanceled = purchase.IsCanceled
            });
        }
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelPurchase(int id, CancellationToken cancellationToken)
        {
            if (!await _purchaseService.CancelPurchase(id, cancellationToken))
                return BadRequest("Cannot cancel purchase .");

            return Ok("Purchase canceled successfully.");
        }

    }
}
