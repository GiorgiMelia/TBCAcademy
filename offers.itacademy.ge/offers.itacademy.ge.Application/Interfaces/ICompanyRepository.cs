using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken);
        Task<Company?> GetCompanyById(int companyId, CancellationToken cancellationToken);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
