using Common.Helper;
using Common.HelperContract;
using QueueService;
using QueueService.Business;
using QueueService.BusinessContract;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IMailMessenger, MailMessenger>();
        services.AddSingleton<IProductsNotification, ProductsNotification>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
