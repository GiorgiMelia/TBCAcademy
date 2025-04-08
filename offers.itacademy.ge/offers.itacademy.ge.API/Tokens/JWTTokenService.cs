using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using offers.itacademy.ge.Domain.entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace offers.itacademy.ge.API.Tokens
{
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
                Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, client.Email!),
                new Claim(ClaimTypes.NameIdentifier, client.Id),
                new Claim("ClientType", client.UserType.ToString()),
                new Claim("BuyerId", client.BuyerId?.ToString() ?? ""),
                new Claim("CompanyId", client.CompanyId?.ToString() ?? ""),]),
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
