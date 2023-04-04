namespace WorkerServiceFraudDetection.Infrastructure.Services
{
    public interface INetworkService
    {
     
        Task<bool> ValidateIpAsync(string ip);
    }
}
