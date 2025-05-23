﻿using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;

namespace offers.itacademy.ge.Web.Controllers
{

    public class OffersController : Controller
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IActionResult> Index()
        {
            var offers = await _offerService.GetAllOffers(CancellationToken.None);
            var offerDtos = offers.Select(o => new OfferDto
            {
                ProductName = o.ProductName,
                Price = o.Price,
                Quantity = o.Quantity,
                CategoryId = o.CategoryId,
                CompanyId = o.CompanyId?? 0,
                EndDate = DateTime.UtcNow,
                ProductDescription = o.ProductDescription,

            }).ToList();

            return View(offerDtos);
    
        }
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _offerService.GetOfferById(id,CancellationToken.None);
            return View(offer);
        }
    }
}