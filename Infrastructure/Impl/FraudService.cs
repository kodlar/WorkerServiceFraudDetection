using WorkerServiceFraudDetection.Infrastructure.Services;
using WorkerServiceFraudDetection.Model;

namespace WorkerServiceFraudDetection.Infrastructure.Impl
{
    public class FraudService : IFraudService, IDisposable
    {
        private readonly ICardService _cardService;
        private readonly INetworkService _networkService;
        public FraudService(ICardService cardService, INetworkService networkService)
        {
            _cardService = cardService;
            _networkService = networkService;
        }


        public async Task<double> Predict(Transaction transaction)
        {
            double _predict = 0.0;
            //kart sahibi isim kontrolü
            var _cardNameValidation = await _cardService.CheckCardNameIsEmptyOrNullAsync(transaction.CardName);
            if (_cardNameValidation)
            {
                _predict = 1.0;
                return _predict;
            }
            //kart sahibi isim uzunluk kontrolü
            var _cardNameLengthValidation = await _cardService.CheckCardNameIsEmptyOrNullAsync(transaction.CardName);
            if (_cardNameLengthValidation)
            {
                _predict = 1.0;
                return _predict;
            }

            //lokasyon kontrol
            var _ipService = await _networkService.ValidateIpAsync(transaction.Ip);
            if (_ipService)
            {
                _predict = 1.0;
                return _predict;
            }

            return _predict;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
