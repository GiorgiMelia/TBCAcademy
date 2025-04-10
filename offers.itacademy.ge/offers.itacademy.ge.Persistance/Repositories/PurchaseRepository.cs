using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;
using ITAcademy.Offers.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ITAcademy.Offers.Persistence.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Purchase> CreatePurchase(Purchase purchase, CancellationToken cancellationToken)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync(cancellationToken);

            return purchase;
        }

        public async Task<IEnumerable<Purchase>> GetActivePurchasesByOfferId(int offerId, CancellationToken cancellationToken)
        {
            return await _context.Purchases
                   .Where(p => p.OfferId == offerId && !p.IsCanceled)
                   .ToListAsync(cancellationToken);
        }

        public async Task<List<Purchase>> GetAllPurchases(CancellationToken cancellationToken)
        {
            return await _context.Purchases.Include(p => p.Offer).Include(p => p.Buyer).ToListAsync(cancellationToken);
        }

        public async Task<Purchase?> GetPurchaseById(int id, CancellationToken cancellationToken)
        {
            return await _context.Purchases.Include(p => p.Offer).Include(p => p.Buyer).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task SaveChanges(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
