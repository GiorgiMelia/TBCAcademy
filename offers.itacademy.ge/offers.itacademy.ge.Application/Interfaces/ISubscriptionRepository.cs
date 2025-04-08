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
        Task<Subscription> CreateSubscription(Subscription subscription,CancellationToken cancellationToken);
        Task<List<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken);
        Task<Subscription?> GetSubscriptionById(int id,CancellationToken cancellationToken);
        Task<bool> DeleteSubscription(int SubscriptionId, int buyerId, CancellationToken cancellationToken);
    
    }
}
