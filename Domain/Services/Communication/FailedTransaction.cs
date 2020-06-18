using System;

namespace Wallet.API.Domain.Services.Communication
{
    public class FailedTransaction
    {
        public Guid TransactionId { get; set; }
        public string Message { get; set; }
    }
}
