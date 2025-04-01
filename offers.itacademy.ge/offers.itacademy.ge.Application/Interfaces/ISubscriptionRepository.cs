using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> CreateSubscription(Subscription subscription);
        Task<List<Subscription>> GetAllSubscriptionsWithDetails();
        Task<Subscription?> GetSubscriptionWithDetailsById(int id);
    }
}
