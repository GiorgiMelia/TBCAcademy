using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;


namespace offers.itacademy.ge.Application.Interfaces
{

    public interface ISubscriptionService
    {
        Task<Subscription> CreateSubscription(SubscriptionDto subscriptionDto);
        Task<List<Subscription>> GetAllSubscriptions();
        Task<Subscription?> GetSubscriptionById(int id);
    }
}
