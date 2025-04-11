using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IBuyerRepository
    {
        Task<int> FindBuyerWithClientId(string clientId);
        Task<List<Buyer>> GetAllBuyers(CancellationToken cancellationToken);
        Task<Buyer?> GetBuyerById(int id, CancellationToken cancellationToken);
        Task SaveImage(string base64,int id);
        Task UpdateBuyer(Buyer buyer, CancellationToken cancellationToken);
    }
}
