using WorkerServiceFraudDetection.Model;

namespace WorkerServiceFraudDetection.Infrastructure.Services
{
    public interface IQueueService
    {
        List<Transaction> GetNextBatch();
    }
}