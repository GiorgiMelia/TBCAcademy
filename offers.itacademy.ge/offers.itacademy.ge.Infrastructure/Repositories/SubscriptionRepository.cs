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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<List<Subscription>> GetAllSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription?> GetSubscriptionById(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
