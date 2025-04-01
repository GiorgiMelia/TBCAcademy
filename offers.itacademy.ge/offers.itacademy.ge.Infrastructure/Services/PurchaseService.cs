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

        public async Task<Purchase> CreatePurchase(PurchaseDto purchaseDto)
        {
            var offer = await _context.Offers.FindAsync(purchaseDto.OfferId);
            if (offer == null)
                throw new Exception($"Offer with Id {purchaseDto.OfferId} not found.");

            if (offer.Quantity < purchaseDto.Quantity)
                throw new Exception($"Not enough quantity in offer. Available: {offer.Quantity}, requested: {purchaseDto.Quantity}");

            offer.Quantity -= purchaseDto.Quantity;

            var purchase = new Purchase
            {
                OfferId = offer.Id,
                BuyerId = purchaseDto.BuyerId,
                Quantity = purchaseDto.Quantity,
                PurchaseDate = DateTime.UtcNow,
                IsCanceled = false
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return await _context.Purchases
    .Include(p => p.Buyer)
    .Include(p => p.Offer)
    .FirstOrDefaultAsync(p => p.Id == purchase.Id);
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            return await _context.Purchases
        .Include(p => p.Buyer)
        .Include(p => p.Offer)
        .ToListAsync();
        }

        public async Task<Purchase?> GetPurchaseById(int id)
        {
            return await _context.Purchases
      .Include(p => p.Buyer)
      .Include(p => p.Offer)
      .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
