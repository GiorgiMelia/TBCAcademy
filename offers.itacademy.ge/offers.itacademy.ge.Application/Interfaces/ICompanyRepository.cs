using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken);
        Task<Company?> GetCompanyById(int companyId, CancellationToken cancellationToken);
        Task SaveChanges(CancellationToken cancellationToken);
        Task SaveImage(string base64, int compId);
    }
}
