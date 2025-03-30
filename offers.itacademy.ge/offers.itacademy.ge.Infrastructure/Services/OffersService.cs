using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Infrastructure.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;

        public OfferService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Offer> CreateOfferAsync(CreateOfferRequest request)
        {
            var offer = new Offer
            {
                ProductId = request.ProductId,
                CategoryId = request.CategoryId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Price = request.Price,
                Quantity = request.Quantity,
                IsArchived = false
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _context.Offers
                .Include(o => o.Category)
                .ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _context.Offers
                .Include(o => o.Category)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
