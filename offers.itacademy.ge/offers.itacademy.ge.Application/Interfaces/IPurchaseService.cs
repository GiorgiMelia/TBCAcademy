using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;


namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> CreatePurchase(PurchaseDto request);
        Task<List<Purchase>> GetAllPurchases();
        Task<Purchase?> GetPurchaseById(int id);
    }
}
