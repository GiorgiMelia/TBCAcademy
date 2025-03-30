using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.API.Models;
namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IOfferService
    {
        Task<Offer> CreateOfferAsync(CreateOfferRequest request);
        Task<List<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
     //  Task<Offer> CreateOfferAsync(string categoryName);
    }
}
