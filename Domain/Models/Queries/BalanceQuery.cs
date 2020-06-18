using System;

namespace Wallet.API.Domain.Models.Queries
{
    public class BalanceQuery
    {
        public Guid PlayerId { get; set; }

        public EAsset? Asset { get; set; } = EAsset.Euro;
    }
}
