using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
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
