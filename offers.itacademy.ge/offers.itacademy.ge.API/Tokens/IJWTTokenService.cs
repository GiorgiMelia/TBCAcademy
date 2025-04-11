using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using offers.itacademy.ge.Domain.entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace offers.itacademy.ge.API.Tokens
{
    public interface IJWTTokenService
    {
        string GenerateToken(Client client, IList<string> roles);



    }
    
}
