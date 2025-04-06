using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Dtos;
using System;
using offers.itacademy.ge.Application.Exceptions;

namespace offers.itacademy.ge.Application.services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository offerRepository;
        private readonly IPurchaseService purchaseService;
        private readonly ICompanyService companyService;
        public OfferService(IOfferRepository context, IPurchaseService purchaseService, ICompanyService companyService)
        {
            offerRepository = context;
            this.purchaseService = purchaseService;
            this.companyService = companyService;
        }

        public async Task<Offer> CreateOffer(OfferDto offerDto, CancellationToken cancellationToken)
        {
            var company = await companyService.GetCompanyById(offerDto.CompanyId, cancellationToken);
            if (company == null)
                throw new NotFoundException($"Company with{offerDto.CompanyId} does not exist");
                if(!company.IsActive)
                throw new WrongRequestException("Company is not Active!");


            var offer = new Offer
            {
                ProductName = offerDto.ProductName,
                ProductDescription = offerDto.ProductDescription,
                CategoryId = offerDto.CategoryId,
                StartDate = DateTime.UtcNow,
                EndDate = offerDto.EndDate,
                Price = offerDto.Price,
                Quantity = offerDto.Quantity,
                IsArchived = false,
                CompanyId = offerDto.CompanyId,
                IsCanceled = false,
            };

            await offerRepository.CreateOffer(offer, cancellationToken);

            return offer;
        }

        public async Task<List<Offer>> GetAllOffers(CancellationToken cancellationToken)
        {
            return await offerRepository.GetAllOffers(cancellationToken);

        }

        public async Task<Offer?> GetOfferById(int id, CancellationToken cancellationToken)
        {
            return await offerRepository.GetOfferById(id, cancellationToken);

        }
        public async Task<bool> CancelOffer(int offerId, CancellationToken cancellationToken)
        {
            var offer = await offerRepository.GetOfferById(offerId, cancellationToken);
            if (offer == null || offer.IsArchived || offer.IsCanceled)
                return false;

            var elapsed = DateTime.UtcNow - offer.StartDate;
            if (elapsed > TimeSpan.FromMinutes(10))
                return false;

            offer.IsCanceled = true;

            return await purchaseService.CancelPurchaseByOffer(offerId, cancellationToken);
        }

        public async Task ArchiveOldOffers(CancellationToken stoppingToken)
        {
            await offerRepository.ArchiveOldOffers(stoppingToken);
        }


    }
}
