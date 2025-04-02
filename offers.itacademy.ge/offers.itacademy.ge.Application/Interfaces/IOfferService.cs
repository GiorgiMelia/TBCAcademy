using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Dtos;
namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IOfferService
    {
        Task<Offer> CreateOffer(OfferDto request);
        Task<List<Offer>> GetAllOffers();
        Task<Offer?> GetOfferById(int id);
        Task<bool> CancelOffer(int offerId);

    }
}
