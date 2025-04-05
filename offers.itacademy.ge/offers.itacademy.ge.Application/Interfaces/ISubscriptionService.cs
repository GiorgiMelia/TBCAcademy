using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;


namespace offers.itacademy.ge.Application.Interfaces
{

    public interface ISubscriptionService
    {
        Task<Subscription> CreateSubscription(SubscriptionDto subscriptionDto, CancellationToken cancellationToken);
        Task<List<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken);
        Task<Subscription?> GetSubscriptionById(int id,CancellationToken cancellationToken);
        Task<bool> DeleteSubscription(int id, CancellationToken cancellationToken);
    }
}
