using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> CreatePurchase(PurchaseDto request, CancellationToken cancellationToken);
        Task<List<Purchase>> GetAllPurchases(CancellationToken cancellationToken);
        Task<Purchase?> GetPurchaseById(int id, CancellationToken cancellationToken);
        Task<bool> CancelPurchase(int purchaseId, int buyerId, CancellationToken cancellationToken);
        Task<bool> CancelPurchaseByOffer(int offerId, CancellationToken cancellationToken);


    }
}
