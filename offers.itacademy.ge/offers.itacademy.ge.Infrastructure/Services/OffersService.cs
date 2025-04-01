using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using offers.itacademy.ge.Application.Dtos;

namespace offers.itacademy.ge.Infrastructure.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;

        public OfferService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Offer> CreateOffer(OfferDto offerDto)
        {

            var offer = new Offer
            {
                ProductName = offerDto.ProductName,
                ProductDescription = offerDto.ProductDescription,
                CategoryId = offerDto.CategoryId,
                StartDate = DateTime.UtcNow,
                EndDate = offerDto.EndDate,
                Price = offerDto.Price,
                Quantity = offerDto.Quantity,
                IsArchived = false
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return await _context.Offers
    .Include(o => o.Category)
    .FirstOrDefaultAsync(o => o.Id == offer.Id);
        }

        public async Task<List<Offer>> GetAllOffers()
        {
            return await _context.Offers
                .Include(o => o.Category)
                .ToListAsync();
        }

        public async Task<Offer?> GetOfferById(int id)
        {
            return await _context.Offers
                .Include(o => o.Category)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
