using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> CreatePurchaseAsync(CreatePurchaseRequest request);
        Task<List<Purchase>> GetAllAsync();
        Task<Purchase?> GetByIdAsync(int id);
    }
}
