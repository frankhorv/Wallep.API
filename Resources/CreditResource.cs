using System;
using System.Collections.Generic;

namespace Wallet.API.Resources
{
    public class SaveTransactionResource
    {
        public Guid PlayerId { get; set; }

        public IList<TransactionResource> Transactions { get; set; } = new List<TransactionResource>();
    }
}