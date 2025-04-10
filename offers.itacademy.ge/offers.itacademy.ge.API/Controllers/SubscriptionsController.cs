using ITAcademy.Offers.API.Extentions;
using ITAcademy.Offers.API.Models;
using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.Offers.API.Controllers
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
        [Authorize(Policy = "MustBuyer")]
        public async Task<ActionResult<SubscriptionResponse>> Create([FromBody] SubscriptionRequest request, CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();
            var subscriptionDto = new SubscriptionDto
            {
                BuyerId = buyerId,
                CategoryId = request.CategoryId,
            };
            var subscription = await _subscriptionService.CreateSubscription(subscriptionDto, cancellationToken);

            var response = new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
            };

            return CreatedAtAction(nameof(GetById), new { subscriptionId = response.Id }, response);
        }

        [HttpGet]
        [Authorize(Policy = "MustAdmin")]
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

        [HttpGet("{subscriptionId}")]
        [Authorize(Policy = "MustAdmin")]
        public async Task<ActionResult<SubscriptionResponse>> GetById(int subscriptionId, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionService.GetSubscriptionById(subscriptionId, cancellationToken);
            if (subscription == null)
                return NotFound();

            return Ok(new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
            });
        }

        [HttpDelete("{subscriptionId}")]
        [Authorize(Policy = "MustBuyer")]
        public async Task<IActionResult> Delete(int subscriptionId, CancellationToken cancellationToken)
        {
            var buyerId = User.GetBuyerId();

            var result = await _subscriptionService.DeleteSubscription(subscriptionId, buyerId, cancellationToken);
            if (!result)
                return NotFound("Subscription not found.");

            return NoContent();
        }
    }
}
