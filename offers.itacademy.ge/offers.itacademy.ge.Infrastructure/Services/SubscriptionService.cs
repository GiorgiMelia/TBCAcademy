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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription> CreateAsync(CreateSubscriptionRequest request)
        {
            var subscription = new Subscription
            {
                BuyerId = request.BuyerId,
                CategoryId = request.CategoryId
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task<List<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.Include(s => s.Category).ToListAsync();
        }

        public async Task<Subscription?> GetByIdAsync(int id)
        {
            return await _context.Subscriptions.Include(s => s.Category)
                                               .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
