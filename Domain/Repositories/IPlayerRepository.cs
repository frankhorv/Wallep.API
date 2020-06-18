using System;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;

namespace Wallet.API.Domain.Repositories
{
    public interface IPlayerRepository
    {
        Task AddAsync(Player player);
        Task<Player> FindByIdAsync(Guid? id);
    }
}
