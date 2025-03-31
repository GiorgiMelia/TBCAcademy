using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{

    public interface ISubscriptionService
    {
        Task<Subscription> CreateAsync(CreateSubscriptionRequest request);
        Task<List<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(int id);
    }
}
