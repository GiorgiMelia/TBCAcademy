using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
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
