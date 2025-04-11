using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace offers.itacademy.ge.API.Extentions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost"
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBuyer", policy =>
                    policy.RequireClaim("ClientType", "Buyer"));

                options.AddPolicy("MustCompany", policy =>
                    policy.RequireClaim("ClientType", "Company"));


                options.AddPolicy("MustAdmin", policy =>
                        policy.RequireRole("Admin"));
            });
            return services;
        }
    }
}
