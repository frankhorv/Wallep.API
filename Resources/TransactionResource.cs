using System;
using System.Collections.Generic;

namespace Wallet.API.Resources
{
    public class TransactionResource
    {
        public Guid TransactionId { get; set; }
        public IList<OperationResource> Operations { get; set; } = new List<OperationResource>();
    }
}
