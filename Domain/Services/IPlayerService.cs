using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Services.Communication;

namespace Wallet.API.Domain.Services
{
    public interface IPlayerService
    {
        Task<PlayerResponse> SaveAsync(Player player);
    }
}