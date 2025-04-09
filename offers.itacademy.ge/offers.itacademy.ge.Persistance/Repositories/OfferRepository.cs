using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
namespace offers.itacademy.ge.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationDbContext _context;

        public OfferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ArchiveOldOffers(CancellationToken stoppingToken)
        {
            var oldOffers = await _context.Offers.Where(o => o.IsArchived != true).Where(o => o.EndDate < DateTime.UtcNow).ToListAsync(stoppingToken);
            foreach (var oldOffer in oldOffers)
            {
                oldOffer.IsArchived = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Offer> CreateOffer(Offer offer, CancellationToken cancellationToken)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<List<Offer>> GetAllOffers(CancellationToken cancellationToken)
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<Offer?> GetOfferById(int id, CancellationToken cancellationToken)
        {
            return await _context.Offers.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Offer>> GetOffersByCompany(int companyId, CancellationToken cancellationToken)
        {
            return await _context.Offers.Where(c => c.CompanyId == companyId).ToListAsync(cancellationToken);

        }

        public async Task<IEnumerable<Offer>> GetSubscribedOffers(int buyerId, CancellationToken cancellationToken)
        {
            var categoryIds = await _context.Subscriptions
            .Where(sub => sub.BuyerId == buyerId)
             .Select(sub => sub.CategoryId)
             .ToListAsync(cancellationToken);

            var offers = await _context.Offers
                .Where(offer => categoryIds.Contains(offer.CategoryId)
                             && offer.IsArchived == false
                             && offer.IsCanceled == false)
                .ToListAsync(cancellationToken);

            return offers;
        }

        public async Task<Offer> UpdateOffer(Offer offer, CancellationToken cancellationToken)
        {
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            return offer;
        }


    }
}
