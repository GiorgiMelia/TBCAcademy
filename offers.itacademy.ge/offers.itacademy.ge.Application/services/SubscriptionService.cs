using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.Application.services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Subscription> CreateSubscription(SubscriptionDto subscritionDto)
        {
            var subscription = new Subscription
            {
                BuyerId = subscritionDto.BuyerId,
                CategoryId = subscritionDto.CategoryId
            };

            return await _subscriptionRepository.CreateSubscription(subscription);

        }

        public async Task<List<Subscription>> GetAllSubscriptions()
        {
            return await _subscriptionRepository.GetAllSubscriptions();
        }

        public async Task<Subscription?> GetSubscriptionById(int id)
        {
            return await _subscriptionRepository.GetSubscriptionById(id);
        }
    }
}
