using System.ComponentModel;

namespace Wallet.API.Domain.Models
{
    public enum EOperation : byte
    {
        [Description("Deposit")]
        Deposit = 1,

        [Description("Stake")]
        Stake = 2,

        [Description("Win")]
        Win = 3,

    }
}
