using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface IOfferService
    {
        Task<Offer> CreateOffer(OfferDto request, CancellationToken cancellationToken);
        Task<List<Offer>> GetAllOffers(CancellationToken cancellationToken);
        Task<Offer?> GetOfferById(int id, CancellationToken cancellationToken);
        Task<bool> CancelOffer(int offerId, int CompanyId, CancellationToken cancellationToken);
        Task ArchiveOldOffers(CancellationToken stoppingToken);
        Task<IEnumerable<Offer>> GetOffersByCompany(int companyId, CancellationToken cancellationToken);
        Task<IEnumerable<Offer>> GetSubscribedOffers(int id, CancellationToken cancellationToken);
    }
}
