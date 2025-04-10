using ITAcademy.Offers.Application.Interfaces;

namespace ITAcademy.Offers.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var offerService = scope.ServiceProvider.GetRequiredService<IOfferService>();
                await offerService.ArchiveOldOffers(stoppingToken);

            }
            await Task.Delay(5000, stoppingToken);
        }
    }
}
