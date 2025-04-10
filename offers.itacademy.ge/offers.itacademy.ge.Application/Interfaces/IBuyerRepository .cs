using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface IBuyerRepository
    {
        Task<int> FindBuyerWithClientId(string clientId);
        Task<List<Buyer>> GetAllBuyers(CancellationToken cancellationToken);
        Task<Buyer?> GetBuyerById(int id, CancellationToken cancellationToken);
        Task SaveImage(string base64, int id);
        Task UpdateBuyer(Buyer buyer, CancellationToken cancellationToken);
    }
}
