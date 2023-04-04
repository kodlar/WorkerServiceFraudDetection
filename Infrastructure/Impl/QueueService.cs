using System.Linq;
using WorkerServiceFraudDetection.Infrastructure.Services;
using WorkerServiceFraudDetection.Model;

namespace WorkerServiceFraudDetection.Infrastructure.Impl
{
    public class QueueService : IQueueService
    {
        public List<Transaction> GetNextBatch()
        {
            return new List<Transaction> { 
            new Transaction() { Amount = Random.Shared.Next(1,1000), 
                CardName = RandomString(2), 
                Id = Random.Shared.Next(1000,100000000), 
                TransactionDate = DateTime.UtcNow, 
                Ip =  IpList().Skip(Random.Shared.Next(1,10000)).Take(1).FirstOrDefault() }
            };
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static uint ParseIP(string ip)
        {
            byte[] b = ip.Split('.').Select(s => Byte.Parse(s)).ToArray();
            if (BitConverter.IsLittleEndian) Array.Reverse(b);
            return BitConverter.ToUInt32(b, 0);
        }

        public static string FormatIP(uint ip)
        {
            byte[] b = BitConverter.GetBytes(ip);
            if (BitConverter.IsLittleEndian) Array.Reverse(b);
            return String.Join(".", b.Select(n => n.ToString()));
        }

        public static string[] IpList()
        {
            string StartIP = "192.168.0.1";
            int IPCount = 10000;

            uint n = ParseIP(StartIP);
            string[] range = new string[IPCount];
            for (uint i = 0; i < IPCount; i++)
            {
                range[i] = FormatIP(n + i);
            }
            return range;
        }
    }


}
