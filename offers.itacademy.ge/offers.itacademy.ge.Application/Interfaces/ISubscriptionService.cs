using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{

    public interface ISubscriptionService
    {
        Task<Subscription> CreateSubscription(SubscriptionDto subscriptionDto, CancellationToken cancellationToken);
        Task<List<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken);
        Task<Subscription?> GetSubscriptionById(int id, CancellationToken cancellationToken);
        Task<bool> DeleteSubscription(int id, int buyerId, CancellationToken cancellationToken);
    }
}
