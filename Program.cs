using WorkerServiceFraudDetection;
using WorkerServiceFraudDetection.Infrastructure.Impl;
using WorkerServiceFraudDetection.Infrastructure.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<FraudDetection>();
        services.AddSingleton<IQueueService, QueueService>();
        services.AddScoped<IFraudService, FraudService>();
    })
    .Build();

host.Run();
