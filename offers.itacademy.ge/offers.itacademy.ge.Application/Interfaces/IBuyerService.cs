using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{

    public interface IBuyerService
    {
        Task<bool> AddMoneyToBuyer(int buyerId, decimal amount);

    }
}
