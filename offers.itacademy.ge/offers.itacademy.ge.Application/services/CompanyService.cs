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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task ActivateCompany(int companyId, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyById(companyId,cancellationToken);
            if (company == null)
                throw new Exception("Company not found");
            company.IsActive = true;

           await _companyRepository.SaveChanges(cancellationToken);

        }

        public async Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken)
        {
            return await _companyRepository.GetAllCompanies(cancellationToken);
        }

        public async Task<Company?> GetCompanyById(int id, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyById(id,cancellationToken);
        }
    }
}
