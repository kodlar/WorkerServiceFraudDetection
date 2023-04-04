using WorkerServiceFraudDetection.Infrastructure.Services;

namespace WorkerServiceFraudDetection.Infrastructure.Impl
{
    public class CardService : ICardService
    {
        public Task<bool> CheckCardNameLengthAsync(string name)
        {
            if (name.Length > 3)
                return Task.FromResult(true);

            return Task.FromResult(false);
        }

        public Task<bool> CheckCardNameIsEmptyOrNullAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        
    }
}
