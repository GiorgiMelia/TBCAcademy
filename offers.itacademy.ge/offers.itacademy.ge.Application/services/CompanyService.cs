using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.services
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
            var company = await _companyRepository.GetCompanyById(companyId, cancellationToken);
            if (company == null)
                throw new NotFoundException("Company not found");
            company.IsActive = true;

            await _companyRepository.SaveChanges(cancellationToken);

        }

        public async Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken)
        {
            return await _companyRepository.GetAllCompanies(cancellationToken);
        }

        public async Task<Company?> GetCompanyById(int id, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyById(id, cancellationToken);
        }
        public async Task UploadImage(string base64, int compId)
        {
            await _companyRepository.SaveImage(base64, compId);
        }
    }
}
