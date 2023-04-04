namespace WorkerServiceFraudDetection.Infrastructure.Services
{
    public interface ICardService
    {
        Task<bool> CheckCardNameLengthAsync(string name);
        Task<bool> CheckCardNameIsEmptyOrNullAsync(string name);
    }
}
