using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Dtos;
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
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Purchase> CreatePurchaseAsync(CreatePurchaseRequest request)
        {
            var offer = await _context.Offers.FindAsync(request.OfferId);
            if (offer == null)
                throw new Exception($"Offer with Id {request.OfferId} not found.");

            if (offer.Quantity < request.Quantity)
                throw new Exception($"Not enough quantity in offer. Available: {offer.Quantity}, requested: {request.Quantity}");

            offer.Quantity -= request.Quantity;

            var purchase = new Purchase
            {
                OfferId = offer.Id,
                BuyerId = request.BuyerId,
                Quantity = request.Quantity,
                PurchaseDate = DateTime.UtcNow,
                IsCanceled = false
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return purchase;
        }

        public async Task<List<Purchase>> GetAllAsync()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task<Purchase?> GetByIdAsync(int id)
        {
            return await _context.Purchases.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
