using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Dtos;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IOfferService
    {
        Task<Offer> CreateOffer(OfferDto request, CancellationToken cancellationToken);
        Task<List<Offer>> GetAllOffers(CancellationToken cancellationToken);
        Task<Offer?> GetOfferById(int id, CancellationToken cancellationToken);
        Task<bool> CancelOffer(int offerId,int CompanyId, CancellationToken cancellationToken);
        Task ArchiveOldOffers(CancellationToken stoppingToken);
        Task<IEnumerable<Offer>> GetOffersByCompany(int companyId, CancellationToken cancellationToken);
        Task<IEnumerable<Offer>> GetSubscribedOffers(int id, CancellationToken cancellationToken);
    }
}
