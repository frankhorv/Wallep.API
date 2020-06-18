using System;
using Wallet.API.Domain.Models;

namespace Wallet.API.Resources
{
    public class OperationResource
    {
        public EOperation Type { get; set; }
        public EAsset Asset { get; set; } // Todo: add AutoMapper to cast it to the respective enum value
        public ulong Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
