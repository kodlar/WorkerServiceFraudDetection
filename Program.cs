using WorkerServiceFraudDetection;
using WorkerServiceFraudDetection.Infrastructure.Impl;
using WorkerServiceFraudDetection.Infrastructure.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<FraudDetection>();
        services.AddSingleton<IFraudService, FraudService>();
        services.AddSingleton<IQueueService, QueueService>();        
        services.AddSingleton<INetworkService, NetworkService>();
        services.AddSingleton<ICardService, CardService>();
    })
    .Build();

host.Run();
