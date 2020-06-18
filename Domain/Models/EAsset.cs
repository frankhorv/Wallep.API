using System.ComponentModel;

namespace Wallet.API.Domain.Models
{
    public enum EAsset : byte
    {
        [Description("EUR")]
        Euro = 1,

        [Description("USD")]
        UsDollar = 2,

        [Description("BTC")]
        Bitcoin = 3,
    }
}
