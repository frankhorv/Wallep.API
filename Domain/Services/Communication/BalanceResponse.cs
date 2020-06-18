using Wallet.API.Domain.Models;

namespace Wallet.API.Domain.Services.Communication
{
    public class BalanceResponse : BaseResponse<Balance>
    {
        public BalanceResponse(Balance balance) : base(balance) { }

        public BalanceResponse(string message) : base(message) { }
    }
}
