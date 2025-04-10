using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.API.Tokens
{
    public interface IJWTTokenService
    {
        string GenerateToken(Client client, IList<string> roles);



    }

}
