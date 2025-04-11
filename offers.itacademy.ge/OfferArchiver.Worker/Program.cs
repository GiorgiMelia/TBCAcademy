using ITAcademy.Offers.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using OfferArchiver.Worker;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Infrastructure.Repositories;

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
