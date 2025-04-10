using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IOfferRepository offerRepository;
        private readonly IBuyerService buyerService;

        public PurchaseService(IPurchaseRepository context, IOfferRepository offerRepository, IBuyerService buyerRepository)
        {
            purchaseRepository = context;
            this.offerRepository = offerRepository;
            buyerService = buyerRepository;
        }

        public async Task<Purchase> CreatePurchase(PurchaseDto purchaseDto, CancellationToken cancellationToken)
        {
            var offer = await offerRepository.GetOfferById(purchaseDto.OfferId, cancellationToken);
            if (offer == null)
                throw new NotFoundException($"Offer with Id {purchaseDto.OfferId} not found.");

            if (offer.Quantity < purchaseDto.Quantity)
                throw new WrongRequestException($"Not enough quantity. Available: {offer.Quantity}");
            if (offer.IsArchived) throw new WrongRequestException("Offer is Archived");
            if (offer.IsCanceled) throw new WrongRequestException("Offer is Caceled");
            var buyer = await buyerService.GetBuyerById(purchaseDto.BuyerId, cancellationToken);
            if (buyer == null)
                throw new NotFoundException($"Buyer with Id {purchaseDto.BuyerId} not found.");
            offer.Quantity -= purchaseDto.Quantity;
            decimal totalCost = offer.Price * purchaseDto.Quantity;
            if (buyer.Balance < totalCost)
                throw new WrongRequestException($"Insufficient funds. Available: {buyer.Balance}, needed: {totalCost}");
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

            await purchaseRepository.CreatePurchase(purchase, cancellationToken);
            return purchase;
        }

        public async Task<List<Purchase>> GetAllPurchases(CancellationToken cancellationToken)
        {
            return await purchaseRepository.GetAllPurchases(cancellationToken);
        }

        public async Task<Purchase?> GetPurchaseById(int id, CancellationToken cancellationToken)
        {
            return await purchaseRepository.GetPurchaseById(id, cancellationToken);

        }
        public async Task<bool> CancelPurchase(int purchaseId, int buyerId, CancellationToken cancellationToken)
        {
            var purchase = await purchaseRepository.GetPurchaseById(purchaseId, cancellationToken);
            if (purchase == null || purchase.BuyerId != buyerId || purchase.IsCanceled || DateTime.UtcNow - purchase.PurchaseDate > TimeSpan.FromMinutes(5))
                return false;

            var offer = await offerRepository.GetOfferById(purchase.OfferId, cancellationToken);
            if (offer != null)
            {
                offer.Quantity += purchase.Quantity;
            }
            var buyer = await buyerService.GetBuyerById(purchase.BuyerId, cancellationToken);

            purchase.IsCanceled = true;
            await buyerService.AddMoneyToBuyer(purchase.BuyerId, offer.Price * purchase.Quantity, cancellationToken);
            await buyerService.UpdateBuyer(buyer, cancellationToken);
            await purchaseRepository.SaveChanges(cancellationToken);

            return true;
        }
        public async Task<bool> CancelPurchaseByOffer(int offerId, CancellationToken cancellationToken)
        {
            var offer = await offerRepository.GetOfferById(offerId, cancellationToken);

            var purchases = await purchaseRepository.GetActivePurchasesByOfferId(offerId, cancellationToken);
            foreach (var purchase in purchases)
            {
                var buyer = await buyerService.GetBuyerById(purchase.BuyerId, cancellationToken);
                if (buyer != null)
                {
                    buyer.Balance += offer.Price * purchase.Quantity;
                    await buyerService.UpdateBuyer(buyer, cancellationToken);
                }

                offer.Quantity += purchase.Quantity;
                purchase.IsCanceled = true;

            }

            await purchaseRepository.SaveChanges(cancellationToken);
            return true;
        }
    }
}