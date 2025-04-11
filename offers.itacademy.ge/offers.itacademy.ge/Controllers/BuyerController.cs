using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Extentions.offers.itacademy.ge.API.Extentions;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
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

        public BuyerController(IOfferService offerService, IBuyerService buyerService, IBuyerRepository buyerRepository)
        {
            _offerService = offerService;
            _buyerService = buyerService;
            _buyerRepository = buyerRepository;
        }

        private async Task<int?> GetBuyerIdAsync()
        {
          

            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (clientId == null) return null;

            return await _buyerRepository.FindBuyerWithClientId(clientId);
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

            TempData["Success"] = $"Added {model.Amount}₾ successfully!";
            return RedirectToAction("AddMoney");
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

            return View(offerDtos);
        }
    }
}
