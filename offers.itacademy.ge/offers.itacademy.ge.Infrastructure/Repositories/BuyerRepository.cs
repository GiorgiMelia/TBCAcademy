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
    public class BuyerRepository : IBuyerRepository
    {
        private readonly ApplicationDbContext _context;

        public BuyerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Buyer?> GetBuyerById(int id)
        {
            return await _context.Buyers.FindAsync(id);
        }

        public async Task UpdateBuyer(Buyer buyer)
        {
            _context.Buyers.Update(buyer);
            await _context.SaveChangesAsync();
        }
    }
}
