using Microsoft.AspNetCore.Mvc;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;

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
        public async Task<ActionResult<SubscriptionResponse>> Create([FromBody] CreateSubscriptionRequest request)
        {
            var subscription = await _subscriptionService.CreateAsync(request);

            var response = new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
                CategoryName = subscription.Category?.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionResponse>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllAsync();

            return Ok(subscriptions.Select(s => new SubscriptionResponse
            {
                Id = s.Id,
                BuyerId = s.BuyerId,
                CategoryId = s.CategoryId,
                CategoryName = s.Category?.Name
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionResponse>> GetById(int id)
        {
            var subscription = await _subscriptionService.GetByIdAsync(id);
            if (subscription == null)
                return NotFound();

            return Ok(new SubscriptionResponse
            {
                Id = subscription.Id,
                BuyerId = subscription.BuyerId,
                CategoryId = subscription.CategoryId,
                CategoryName = subscription.Category?.Name
            });
        }
    }
}
