using System;
using System.Collections.Generic;

namespace Wallet.API.Domain.Services.Communication
{
    public class TransactionStatistics
    {
        public IList<Guid> AppliedTransactions { get; set; } = new List<Guid>();
        public IList<FailedTransaction> FailedTransactions { get; set; } = new List<FailedTransaction>();
    }
}
