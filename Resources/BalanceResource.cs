using Wallet.API.Domain.Models;

namespace Wallet.API.Resources
{
    public class BalanceResource
    {
        public EAsset Asset { get; set; } // Todo: add AutoMapper to cast it to the respective enum value
        public ulong Amount { get; set; }
    }
}