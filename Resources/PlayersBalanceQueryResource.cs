using System;
using Wallet.API.Domain.Models;

namespace Wallet.API.Resources
{
    public class PlayersBalanceQueryResource
    {
        public Guid PlayerId { get; set; }
        public EAsset? Asset { get; set; } // Todo: add AutoMapper to cast it to the respective enum value
    }
}