using Wallet.API.Domain.Models;

namespace Wallet.API.Domain.Services.Communication
{
    public class PlayerResponse : BaseResponse<Player>
    {
        public PlayerResponse(Player player) : base(player) { }

        public PlayerResponse(string message) : base(message) { }
    }
}