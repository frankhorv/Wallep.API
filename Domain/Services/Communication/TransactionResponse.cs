namespace Wallet.API.Domain.Services.Communication
{
    public class TransactionResponse : BaseResponse<TransactionStatistics>
    {
        public TransactionResponse(TransactionStatistics statistics) : base(statistics) { }

        public TransactionResponse(string message) : base(message) { }
    }
}
