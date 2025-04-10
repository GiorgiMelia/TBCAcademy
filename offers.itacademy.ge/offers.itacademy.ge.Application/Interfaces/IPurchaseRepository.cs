using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase> CreatePurchase(Purchase purchase, CancellationToken cancellationToken);
        Task<IEnumerable<Purchase>> GetActivePurchasesByOfferId(int offerId, CancellationToken cancellationToken);
        Task<List<Purchase>> GetAllPurchases(CancellationToken cancellationToken);
        Task<Purchase?> GetPurchaseById(int id, CancellationToken cancellationToken);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
