using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface IOfferRepository
    {
        Task ArchiveOldOffers(CancellationToken stoppingToken);
        Task<Offer> CreateOffer(Offer offer, CancellationToken cancellationToken);
        Task<List<Offer>> GetAllOffers(CancellationToken cancellationToken);
        Task<Offer?> GetOfferById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Offer>> GetOffersByCompany(int companyId, CancellationToken cancellationToken);
        Task<IEnumerable<Offer>> GetSubscribedOffers(int buyerId, CancellationToken cancellationToken);
        Task<Offer> UpdateOffer(Offer offer, CancellationToken cancellationToken);


    }
}
