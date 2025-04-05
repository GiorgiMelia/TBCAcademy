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
        public async Task<ActionResult<SubscriptionResponse>> Create([FromBody] SubscriptionRequest request, CancellationToken cancellationToken)
        {
            SubscriptionDto subscriptionDto = new SubscriptionDto
            {
                BuyerId = request.BuyerId,
                CategoryId = request.CategoryId,
            };
            var subscription = await _subscriptionService.CreateSubscription(subscriptionDto,cancellationToken);

            var response = new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
                };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptions(cancellationToken);

            return Ok(subscriptions.Select(s => new SubscriptionResponse
            {
                Id = s.Id,
                BuyerId = s.BuyerId,
                CategoryId = s.CategoryId,
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionResponse>> GetById(int id,CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionService.GetSubscriptionById(id, cancellationToken);
            if (subscription == null)
                return NotFound();

            return Ok(new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _subscriptionService.DeleteSubscription(id,cancellationToken);
            if (!result)
                return NotFound("Subscription not found.");

            return NoContent(); 
        }
    }
}
