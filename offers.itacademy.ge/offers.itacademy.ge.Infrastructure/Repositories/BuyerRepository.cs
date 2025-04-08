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
    public class BuyerRepository : IBuyerRepository
    {
        private readonly ApplicationDbContext _context;

        public BuyerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> FindBuyerWithClientId(string clientId)
        {
            var buyer = await _context.Users.FirstOrDefaultAsync(u => u.Id == clientId);

            return buyer.BuyerId?? 0;
        }

        public async Task<List<Buyer>> GetAllBuyers(CancellationToken cancellationToken)
        {
            return await _context.Buyers.ToListAsync(cancellationToken);
        }

        public async Task<Buyer?> GetBuyerById(int id, CancellationToken cancellationToken)
        {
            return await _context.Buyers.FindAsync(id,cancellationToken);
        }

      
        public async Task UpdateBuyer(Buyer buyer, CancellationToken cancellationToken)
        {
            _context.Buyers.Update(buyer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
