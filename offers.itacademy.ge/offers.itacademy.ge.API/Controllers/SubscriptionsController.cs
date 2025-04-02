using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.API.Models;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionResponse>> Create([FromBody] SubscriptionRequest request)
        {
            SubscriptionDto subscriptionDto = new SubscriptionDto
            {
                BuyerId = request.BuyerId,
                CategoryId = request.CategoryId,
            };
            var subscription = await _subscriptionService.CreateSubscription(subscriptionDto);

            var response = new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
                };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionResponse>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptions();

            return Ok(subscriptions.Select(s => new SubscriptionResponse
            {
                Id = s.Id,
                BuyerId = s.BuyerId,
                CategoryId = s.CategoryId,
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionResponse>> GetById(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription == null)
                return NotFound();

            return Ok(new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
            });
        }
    }
}
