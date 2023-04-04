namespace WorkerServiceFraudDetection.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal Amount { get; set; }
        public string CardName { get; set; }
        public string  Ip { get; set; }
    }
}
