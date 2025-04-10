using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Application.services;
using ITAcademy.Offers.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;



namespace ITAcademy.Offers.Infrastructure.DIConfiguration
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IOfferService, OfferService>();
            Services.AddScoped<IPurchaseService, PurchaseService>();
            Services.AddScoped<ISubscriptionService, SubscriptionService>();
            Services.AddScoped<IOfferRepository, OfferRepository>();
            Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<IBuyerRepository, BuyerRepository>();
            Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            Services.AddScoped<ICompanyRepository, CompanyRepository>();
            Services.AddScoped<ICompanyService, CompanyService>();
            Services.AddScoped<IBuyerService, BuyerService>();
            return Services;
        }
    }
}
