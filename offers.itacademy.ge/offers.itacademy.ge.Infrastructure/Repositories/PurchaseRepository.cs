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
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Purchase> CreatePurchase(Purchase purchase)
        {
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
