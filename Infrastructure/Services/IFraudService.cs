using Transaction = WorkerServiceFraudDetection.Model.Transaction;

namespace WorkerServiceFraudDetection.Infrastructure.Services
{
    public interface IFraudService
    {
         Task<double> Predict(Transaction transaction);
    }
}