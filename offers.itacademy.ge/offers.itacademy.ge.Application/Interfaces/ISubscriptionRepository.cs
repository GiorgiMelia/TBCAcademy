using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> CreateSubscription(Subscription subscription, CancellationToken cancellationToken);
        Task<List<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken);
        Task<Subscription?> GetSubscriptionById(int id, CancellationToken cancellationToken);
        Task<bool> DeleteSubscription(int SubscriptionId, int buyerId, CancellationToken cancellationToken);

    }
}
