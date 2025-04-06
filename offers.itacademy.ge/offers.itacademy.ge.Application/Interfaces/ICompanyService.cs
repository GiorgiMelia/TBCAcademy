using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface ICompanyService
    {
        Task ActivateCompany(int companyId, CancellationToken cancellationToken);
        Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken);
        Task <Company?> GetCompanyById(int id, CancellationToken cancellationToken);
    }
}
