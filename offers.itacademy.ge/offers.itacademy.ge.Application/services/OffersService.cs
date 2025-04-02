using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Dtos;
using System;

namespace offers.itacademy.ge.Application.services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository offerRepository;
        private readonly IPurchaseService purchaseService;
        public OfferService(IOfferRepository context, IPurchaseService purchaseService)
        {
            offerRepository = context;
            this.purchaseService = purchaseService;
        }

        public async Task<Offer> CreateOffer(OfferDto offerDto)
        {

            var offer = new Offer
            {
                ProductName = offerDto.ProductName,
                ProductDescription = offerDto.ProductDescription,
                CategoryId = offerDto.CategoryId,
                StartDate = DateTime.UtcNow,
                EndDate = offerDto.EndDate,
                Price = offerDto.Price,
                Quantity = offerDto.Quantity,
                IsArchived = false
            };

            await offerRepository.CreateOffer(offer);

            return offer;
        }

        public async Task<List<Offer>> GetAllOffers()
        {
            return await offerRepository.GetAllOffers();

        }

        public async Task<Offer?> GetOfferById(int id)
        {
            return await offerRepository.GetOfferById(id);

        }
        public async Task<bool> CancelOffer(int offerId)
        {
            var offer = await offerRepository.GetOfferById(offerId);
            if (offer == null || offer.IsArchived)
                return false;

            var elapsed = DateTime.UtcNow - offer.StartDate;
            if (elapsed > TimeSpan.FromMinutes(10))
                return false;

            offer.IsArchived = true;

            return await purchaseService.CancelPurchaseByOffer(offerId);
        }
    }
}
