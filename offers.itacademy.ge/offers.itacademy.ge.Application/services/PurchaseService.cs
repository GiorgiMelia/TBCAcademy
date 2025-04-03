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
        private readonly IBuyerRepository buyerRepository;

        public PurchaseService(IPurchaseRepository context,IOfferRepository offerRepository,IBuyerRepository buyerRepository)
        {
            purchaseRepository = context;
            this.offerRepository = offerRepository;
            this.buyerRepository = buyerRepository;
        }

        public async Task<Purchase> CreatePurchase(PurchaseDto purchaseDto)
        {
            var offer = await offerRepository.GetOfferById(purchaseDto.OfferId);
            if (offer == null)
                throw new Exception($"Offer with Id {purchaseDto.OfferId} not found.");

            if (offer.Quantity < purchaseDto.Quantity)
                throw new Exception($"Not enough quantity. Available: {offer.Quantity}");
            if (offer.IsArchived) throw new Exception("Offer is Archived");
            var buyer = await buyerRepository.GetBuyerById(purchaseDto.BuyerId);
            if (buyer == null)
                throw new Exception($"Buyer with Id {purchaseDto.BuyerId} not found.");
            offer.Quantity -= purchaseDto.Quantity;
            decimal totalCost = offer.Price * purchaseDto.Quantity;
            if (buyer.Balance < totalCost)
                throw new Exception($"Insufficient funds. Available: {buyer.Balance}, needed: {totalCost}");
            buyer.Balance -= totalCost;
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
        public async Task<bool> CancelPurchase(int purchaseId)
        {
            var purchase = await purchaseRepository.GetPurchaseById(purchaseId);
            if (purchase == null || purchase.IsCanceled|| DateTime.UtcNow - purchase.PurchaseDate > TimeSpan.FromMinutes(5))
                return false;

            var offer = await offerRepository.GetOfferById(purchase.OfferId);
            if (offer != null)
            {
                offer.Quantity += purchase.Quantity;
            }

            purchase.IsCanceled = true;
            await purchaseRepository.SaveChanges();

            return true;
        }
        public async Task<bool> CancelPurchaseByOffer(int offerId)
        {
            var offer = await offerRepository.GetOfferById(offerId);

            var purchases = await purchaseRepository.GetActivePurchasesByOfferId(offerId);
            foreach (var purchase in purchases)
            {
                var buyer = await buyerRepository.GetBuyerById(purchase.BuyerId);
                if (buyer != null)
                {
                    buyer.Balance += offer.Price * purchase.Quantity;
                    await buyerRepository.UpdateBuyer(buyer);
                }

                offer.Quantity += purchase.Quantity;
                purchase.IsCanceled = true;

            }

            await purchaseRepository.SaveChanges();
            return true;
        }
    }
}