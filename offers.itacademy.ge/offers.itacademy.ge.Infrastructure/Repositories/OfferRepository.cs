using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var oldOffers = await _context.Offers.Where(o => o.IsArchived != true).ToListAsync(stoppingToken);
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
        public async Task<Offer> UpdateOffer(Offer offer, CancellationToken cancellationToken)
        {
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

      
    }
}
