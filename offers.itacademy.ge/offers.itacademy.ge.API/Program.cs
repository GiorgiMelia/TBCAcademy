using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using offers.itacademy.ge.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.API.Middlewares;
using offers.itacademy.ge.API.Tokens;
using offers.itacademy.ge.Infrastructure.DIConfiguration;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using offers.itacademy.ge.API.Extentions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCoreServices();
builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();
builder.Services.AddIdentity<Client, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.Configure<JWTTokenOptins>(builder.Configuration.GetSection("JWTOptins"));
builder.Services.AddTokenAuthentication(builder.Configuration.GetSection("JWTOptins").GetSection(nameof(JWTTokenOptins.Secret)).Value);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
var app = builder.Build();

app.MapHealthChecks("/healthz");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
