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
         string GenerateToken(Client client);



    }
    public class JWTTokenService : IJWTTokenService
    {

        private readonly IOptions<JWTTokenOptins> _options;

        public JWTTokenService(IOptions<JWTTokenOptins> options)
        {
            _options = options;
        }

        public string GenerateToken(Client client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.UtcNow.AddMinutes(_options.Value.ExpireTime),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
