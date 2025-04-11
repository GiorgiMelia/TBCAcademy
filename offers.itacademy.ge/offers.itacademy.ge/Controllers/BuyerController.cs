using ITAcademy.Offers.Web.Models.offers.itacademy.ge.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Extentions.offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Web.Models;
using System.Security.Claims;

namespace offers.itacademy.ge.Web.Controllers
{
    [Authorize]
    public class BuyerController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly IBuyerService _buyerService;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IPurchaseService _purchaseService;

        public BuyerController(IOfferService offerService, IBuyerService buyerService, IBuyerRepository buyerRepository, IPurchaseService purchaseService)
        {
            _offerService = offerService;
            _buyerService = buyerService;
            _buyerRepository = buyerRepository;
            _purchaseService = purchaseService;
        }

        private async Task<int?> GetBuyerIdAsync()
        {
          

            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (clientId == null) return null;

            return await _buyerRepository.FindBuyerWithClientId(clientId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMoney(AddMoneyViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var buyerId = await GetBuyerIdAsync();
            if (buyerId == null)
            {
                ModelState.AddModelError("", "Buyer not found or you are not authorized.");
                return View(model);
            }

            var result = await _buyerService.AddMoneyToBuyer(buyerId.Value, model.Amount, cancellationToken);

            if (!result)
            {
                ModelState.AddModelError("", "Money could not be added.");
                return View(model);
            }

            var buyer = await _buyerService.GetBuyerById(buyerId.Value, cancellationToken);
            TempData["Success"] = $"Added {model.Amount}₾ successfully! New balance: {buyer.Balance}₾";

            return RedirectToAction("AddMoney");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(BuyViewModel model, CancellationToken cancellationToken)
        {
            var buyerId = await GetBuyerIdAsync();
            if (buyerId == null) return Unauthorized();

            var purchase = new PurchaseDto
            {
                BuyerId = (int)buyerId,
                OfferId = model.OfferId,
                Quantity = model.QuantityToBuy,
            };
            var result = await _purchaseService.CreatePurchase(purchase,cancellationToken);
            if (result is not Purchase)
            {
                TempData["Error"] = "Purchase failed.";
                return RedirectToAction("Dashboard");
            }

            TempData["Success"] = $"Purchase successful! Your new balance is {result.Buyer.Balance}₾.";
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> AddMoney()
        {
            var buyerId = await GetBuyerIdAsync();
            if (buyerId == null) return Unauthorized();

            var buyer = await _buyerService.GetBuyerById(buyerId.Value, CancellationToken.None);
            if (buyer == null) return Unauthorized();

            return View(new AddMoneyViewModel
            {
                CurrentBalance = buyer.Balance
            });
        }


        public async Task<IActionResult> Dashboard()
        {
            var buyerId = await GetBuyerIdAsync();
            if (buyerId == null) return Unauthorized();

            var offers = await _offerService.GetSubscribedOffers(buyerId.Value, CancellationToken.None);
            var offerDtos = offers.Select(o => new OfferDto
            {
                ProductName = o.ProductName,
                ProductDescription = o.ProductDescription,
                EndDate = o.EndDate,
                CategoryId = o.CategoryId,
                CompanyId = o.CompanyId ?? 0,
                Price = o.Price,
                Quantity = o.Quantity
            }).ToList();
            ViewData["OfferMap"] = offers.ToDictionary(o => o.ProductName, o => o.Id);
            return View(offerDtos);
        }
    }
}
