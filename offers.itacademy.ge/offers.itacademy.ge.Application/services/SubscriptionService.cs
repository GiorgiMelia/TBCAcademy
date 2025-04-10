using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Subscription> CreateSubscription(SubscriptionDto subscritionDto, CancellationToken cancellationToken)
        {

            var subscription = new Subscription
            {
                BuyerId = subscritionDto.BuyerId,
                CategoryId = subscritionDto.CategoryId
            };

            return await _subscriptionRepository.CreateSubscription(subscription, cancellationToken);

        }

        public async Task<List<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken)
        {
            return await _subscriptionRepository.GetAllSubscriptions(cancellationToken);
        }

        public async Task<Subscription?> GetSubscriptionById(int id, CancellationToken cancellationToken)
        {
            var sub = await _subscriptionRepository.GetSubscriptionById(id, cancellationToken);
            if (sub == null) throw new NotFoundException("Subscription not found");
            return sub;
        }
        public async Task<bool> DeleteSubscription(int SubscripitonId, int BuyerId, CancellationToken cancellationToken)
        {

            return await _subscriptionRepository.DeleteSubscription(SubscripitonId, BuyerId, cancellationToken);
        }
    }
}
