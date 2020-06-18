using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Repositories;
using Wallet.API.Persistence.Contexts;

namespace Wallet.API.Persistence.Repositories
{
    /// Class:  PlayerRepository
    ///
    /// A player repository.
	public class PlayerRepository : BaseRepository, IPlayerRepository
	{
		public PlayerRepository(AppDbContext context) : base(context) { }

        /// Function:   AddAsync
        ///
        /// Stores an instance of player in the repository asynchronously.
        ///
        /// Parameters:
        /// player -    The player.
        ///
        /// Returns:    An asynchronous result.
		public async Task AddAsync(Player player)
		{
			await _context.Players.AddAsync(player);
		}

        /// Function:   FindByIdAsync
        ///
        /// Searches for the player by identifier asynchronously.
        ///
        /// Parameters:
        /// id -    The identifier.
        ///
        /// Returns:    An asynchronous result that yields the find by identifier.
        public async Task<Player> FindByIdAsync(Guid? id)
        {
            return await _context.Players
                                    .Include(p => p.Balances)
                                    .Include(p => p.Transactions)
                                    .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}