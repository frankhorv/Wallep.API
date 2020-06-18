using System;
using System.ComponentModel.DataAnnotations;

namespace Wallet.API.Domain.Models
{
    public class Balance
    {
        public EAsset Asset { get; set; }

        public ulong Amount { get; set; }

        public Guid PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
