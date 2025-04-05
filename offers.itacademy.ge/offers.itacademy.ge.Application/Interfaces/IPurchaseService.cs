using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;


namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> CreatePurchase(PurchaseDto request, CancellationToken cancellationToken);
        Task<List<Purchase>> GetAllPurchases(CancellationToken cancellationToken);
        Task<Purchase?> GetPurchaseById(int id, CancellationToken cancellationToken);
        Task<bool> CancelPurchase(int purchaseId, CancellationToken cancellationToken);
        Task<bool> CancelPurchaseByOffer(int offerId, CancellationToken cancellationToken);


    }
}
