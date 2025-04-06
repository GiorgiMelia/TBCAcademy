using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.services
{
    public class BuyerService : IBuyerService
    {
        private readonly IBuyerRepository _buyerRepository;

        public BuyerService(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task<bool> AddMoneyToBuyer(int buyerId, decimal amount,CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.GetBuyerById(buyerId, cancellationToken);
            if (buyer == null)
                return false;

            buyer.Balance += amount;
           await _buyerRepository.UpdateBuyer(buyer, cancellationToken);
            return true;
        }
        public async Task<List<Buyer>> GetAllBuyers(CancellationToken cancellationToken)
        {
            return await _buyerRepository.GetAllBuyers(cancellationToken);
        }

        public async Task<Buyer?> GetBuyerById(int id, CancellationToken cancellationToken)
        {
            return await _buyerRepository.GetBuyerById(id, cancellationToken);
        }

        public async Task UpdateBuyer(Buyer buyer, CancellationToken cancellationToken)
        {
             await _buyerRepository.UpdateBuyer(buyer, cancellationToken);
        }
    }
}
