using Microsoft.Extensions.Options;
using offers.itacademy.ge.Domain.entities;
using System.Security.Claims;
using System.Text;

namespace offers.itacademy.ge.API.Tokens
{
    public interface IJWTTokenService
    {
         Task<string> GenerateToken(Client client);




    }
    public class JWTTokenService : IJWTTokenService
    {
        public async Task<string> GenerateToken(Client client)
        {


            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpirationInMInutes),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
