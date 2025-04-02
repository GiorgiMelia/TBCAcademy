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
        Task<Buyer?> GetBuyerById(int id);
        Task UpdateBuyer(Buyer buyer);
    }
}
