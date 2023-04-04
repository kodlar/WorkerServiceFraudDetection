using WorkerServiceFraudDetection.Infrastructure.Services;
using WorkerServiceFraudDetection.Model;

namespace WorkerServiceFraudDetection.Infrastructure.Impl
{
    public class QueueService : IQueueService
    {
        public List<Transaction> GetNextBatch()
        {
            throw new NotImplementedException();
        }
    }
}
