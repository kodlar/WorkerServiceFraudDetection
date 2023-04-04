using Dapper;
using Microsoft.Data.SqlClient;
using WorkerServiceFraudDetection.Infrastructure.Services;
using WorkerServiceFraudDetection.Model;

namespace WorkerServiceFraudDetection
{
    public class FraudDetection : BackgroundService
    {
        private readonly ILogger<FraudDetection> _logger;        
        private readonly SqlConnection _sqlConnection;
        private readonly IQueueService _transactionsQueue;
        private readonly IFraudService _fraudService;
        public FraudDetection(ILogger<FraudDetection> logger,  IQueueService transactionsQueue, IFraudService fraudService)
        {
            _logger = logger;            
            _sqlConnection = new SqlConnection($"Data Source=YOUR_DATABASE_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;Application Name=YOUR_APP_NAME");
            _sqlConnection.Open();
            _fraudService = fraudService;
            _transactionsQueue = transactionsQueue;
        }


        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Herhangi bir mesaj kuyruğundan transaction listesini çek
                List<Transaction> transactions = _transactionsQueue.GetNextBatch();

                //Arka planda transactionları işle
                Task.Run(() => ProcessTransactions(transactions));

                // bir sonraki döngüye geçmeden 2 saniye bekle
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private async Task ProcessTransactions(List<Transaction> transactions)
        {

            foreach (Transaction transaction in transactions)
            {
                // Fraud durumu kontrol et
                if (await IsSuspiciousTransaction(transaction))
                {
                    // Add the transaction to the database for further review
                    _logger.LogInformation("Potential fraud detected: " + transaction.TransactionDate);
                    _sqlConnection.ExecuteAsync("INSERT INTO FRAUD_TRANSACTIONS (TransactionDate, Amount, Id) VALUES (@0, @1, @2)", new { transaction?.TransactionDate, transaction.Amount, transaction.Id });
                    
                    //Başka bir kuyruğa alınabilir...
                }
            }
        }

        private async Task<bool> IsSuspiciousTransaction(Transaction transaction)
        {
            // Burada eldeki modele göre karşılaştırma yapılabilir...
            double prediction = await _fraudService.Predict(transaction);

            if (prediction > 0.5)
            {
                // buradaki tehlike %50'den büyükse transactionu işaretle
                _logger.LogInformation("Potential fraud detected: " + transaction.Id);
                return true;
            }
            else
            {
                //sorun yoksa devam et.
                _logger.LogInformation("No potential fraud detected: " + transaction.Id);
                return false;
            }
        }
    }
}