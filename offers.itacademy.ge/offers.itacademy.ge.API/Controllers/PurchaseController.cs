using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;

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
        public async Task<ActionResult<PurchaseResponse>> Create([FromBody] CreatePurchaseRequest request)
        {
            var purchase = await _purchaseService.CreatePurchaseAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.Id,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                IsCanceled = purchase.IsCanceled
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseResponse>>> GetAll()
        {
            var purchases = await _purchaseService.GetAllAsync();

            return Ok(purchases.Select(p => new PurchaseResponse
            {
                Id = p.Id,
                OfferId = p.Id,
                BuyerId = p.BuyerId,
                Quantity = p.Quantity,
                PurchaseDate = p.PurchaseDate,
                IsCanceled = p.IsCanceled
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseResponse>> GetById(int id)
        {
            var purchase = await _purchaseService.GetByIdAsync(id);
            if (purchase == null)
                return NotFound();

            return Ok(new PurchaseResponse
            {
                Id = purchase.Id,
                OfferId = purchase.Id,
                BuyerId = purchase.BuyerId,
                Quantity = purchase.Quantity,
                PurchaseDate = purchase.PurchaseDate,
                IsCanceled = purchase.IsCanceled
            });
        }
    }
}
