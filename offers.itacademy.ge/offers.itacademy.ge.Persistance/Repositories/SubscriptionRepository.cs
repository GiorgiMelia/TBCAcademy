﻿using ITAcademy.Offers.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Exceptions;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
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

        public async Task<Subscription> CreateSubscription(Subscription subscription, CancellationToken cancellationToken)
        {
            if (_context.Subscriptions.Any(c => c.BuyerId == subscription.BuyerId && c.CategoryId == subscription.CategoryId))
                throw new WrongRequestException("Category already subscribed!");
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync(cancellationToken);
            return subscription;
        }

        public async Task<bool> DeleteSubscription(int SubscriptionId, int BuyerId, CancellationToken cancellationToken)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.BuyerId == BuyerId, cancellationToken);

            if (subscription == null || subscription.BuyerId != BuyerId)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken)
        {
            return await _context.Subscriptions.ToListAsync(cancellationToken);
        }

        public async Task<Subscription?> GetSubscriptionById(int id, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}
