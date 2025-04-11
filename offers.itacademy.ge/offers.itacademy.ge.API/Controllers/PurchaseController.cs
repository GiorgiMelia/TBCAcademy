using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Domain.entities;
using Microsoft.AspNetCore.Authorization;
using offers.itacademy.ge.API.Extentions.offers.itacademy.ge.API.Extentions;

namespace offers.itacademy.ge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IBuyerService _buyerService;
        public PurchaseController(IPurchaseService purchaseService, IBuyerService buyerService)
        {
            _purchaseService = purchaseService;
            _buyerService = buyerService;
        }

        [HttpPost]
        [Authorize(Policy = "MustBuyer")]
        public async Task<ActionResult<PurchaseResponse>> Create([FromBody] PurchaseRequest request, CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();
            var purchasedto = new PurchaseDto
            {
                BuyerId = buyerId,
                OfferId = request.OfferId,
                Quantity = request.Quantity,
            };
            var purchase = await _purchaseService.CreatePurchase(purchasedto,cancellationToken);

            return CreatedAtAction(nameof(GetById), new { purchaseId = purchase.Id }, new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.OfferId,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                TotalPayment = purchase.Quantity * purchase.Offer.Price,
                IsCanceled = purchase.IsCanceled
            });
        }



        [Authorize(Policy = "MustAdmin")]
        [HttpGet]
        public async Task<ActionResult<List<PurchaseResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var purchases = await _purchaseService.GetAllPurchases(cancellationToken);

            return Ok(purchases.Select(purchase => new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.OfferId,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                TotalPayment = purchase.Quantity * purchase.Offer.Price,
                IsCanceled = purchase.IsCanceled
            }));
        }

        [Authorize(Policy = "MustAdmin")]

        [HttpGet("{purchaseId}")]
        public async Task<ActionResult<PurchaseResponse>> GetById(int purchaseId, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseService.GetPurchaseById(purchaseId, cancellationToken);
            if (purchase == null)
                return NotFound();

            return Ok(new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.OfferId,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                TotalPayment = purchase.Quantity * purchase.Offer.Price,
                IsCanceled = purchase.IsCanceled
            });
        }

        [Authorize(Policy = "MustBuyer")]
        [HttpPost("{purchaseId}/cancel")]
        public async Task<IActionResult> CancelPurchase(int purchaseId, CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();
            if (!await _purchaseService.CancelPurchase(purchaseId,buyerId, cancellationToken))
                return BadRequest("Cannot cancel purchase .");

            return Ok("Purchase canceled successfully.");
        }

    }
}
