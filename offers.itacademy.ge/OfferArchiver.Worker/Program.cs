using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Application.services;
using ITAcademy.Offers.Persistence.Data;
using ITAcademy.Offers.Persistence.Repositories;
using ITAcademy.Offers.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IBuyerService, BuyerService>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();


    })
    .Build()
    .Run();
var host = builder.Build();
host.Run();
