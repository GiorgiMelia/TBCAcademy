using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{

    public interface IBuyerService
    {
        Task<bool> AddMoneyToBuyer(int buyerId, decimal amount, CancellationToken cancellationToken);
        Task<List<Buyer>> GetAllBuyers(CancellationToken cancellationToken);
        Task<Buyer?> GetBuyerById(int id, CancellationToken cancellationToken);
        Task UpdateBuyer(Buyer buyer, CancellationToken cancellationToken);
        Task UploadImage(string base64, int buyerId);
    }
}
