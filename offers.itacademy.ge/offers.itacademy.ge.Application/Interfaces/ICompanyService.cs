using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface ICompanyService
    {
        Task ActivateCompany(int companyId, CancellationToken cancellationToken);
        Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken);
        Task<Company?> GetCompanyById(int id, CancellationToken cancellationToken);
        Task UploadImage(string base64, int compId);
    }
}
