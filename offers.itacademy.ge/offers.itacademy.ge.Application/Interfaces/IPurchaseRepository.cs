using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase> CreatePurchase(Purchase purchase);
        Task<List<Purchase>> GetAllPurchases();
        Task<Purchase?> GetPurchaseById(int id);
    }
}
