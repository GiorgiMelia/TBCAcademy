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
        Task<Offer> CreateOffer(Offer offer);
        Task<List<Offer>> GetAllOffers();
        Task<Offer?> GetOfferById(int id);
    }
}
