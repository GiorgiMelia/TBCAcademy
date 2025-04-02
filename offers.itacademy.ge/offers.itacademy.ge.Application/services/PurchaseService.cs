using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.Application.services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IOfferRepository offerRepository;

        public PurchaseService(IPurchaseRepository context,IOfferRepository offerRepository)
        {
            purchaseRepository = context;
            this.offerRepository = offerRepository;
        }

        public async Task<Purchase> CreatePurchase(PurchaseDto purchaseDto)
        {
            var offer = await offerRepository.GetOfferById(purchaseDto.OfferId);
            if (offer == null)
                throw new Exception($"Offer with Id {purchaseDto.OfferId} not found.");

            if (offer.Quantity < purchaseDto.Quantity)
                throw new Exception($"Not enough quantity in offer. Available: {offer.Quantity}, requested: {purchaseDto.Quantity}");

            offer.Quantity -= purchaseDto.Quantity;

            var purchase = new Purchase
            {
                OfferId = offer.Id,
                BuyerId = purchaseDto.BuyerId,
                Quantity = purchaseDto.Quantity,
                PurchaseDate = DateTime.UtcNow,
                IsCanceled = false,
            };

            await purchaseRepository.CreatePurchase(purchase);
            return purchase;
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            return await purchaseRepository.GetAllPurchases();
        }

        public async Task<Purchase?> GetPurchaseById(int id)
        {
            return await purchaseRepository.GetPurchaseById(id);

        }
    }
}
