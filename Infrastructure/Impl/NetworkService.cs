using WorkerServiceFraudDetection.Infrastructure.Services;

namespace WorkerServiceFraudDetection.Infrastructure.Impl
{
    public class NetworkService : INetworkService
    {
        public Task<bool> ValidateIpAsync(string ip)
        {
            List<string> ips = new List<string>() { "127.0.0.1", "172.3.15.12" };
            return Task.FromResult(ips.Contains(ip));
        }
    }
}
