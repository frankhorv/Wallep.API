using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Models.Queries;
using Wallet.API.Domain.Repositories;
using Wallet.API.Persistence.Contexts;

namespace Wallet.API.Persistence.Repositories
{
    /// Class:  BalanceRepository
    ///
    /// A balance repository.
	public class BalanceRepository : BaseRepository, IBalanceRepository
	{
		public BalanceRepository(AppDbContext context) : base(context) { }

        /// Function:   ListAsync
        ///
        /// List player balances asynchronously.
        ///
        /// Parameters:
        /// balanceQuery -  The balance query.
        ///
        /// Returns:    An asynchronous result that yields the list.
        public async Task<IEnumerable<Balance>> ListAsync(BalanceQuery balanceQuery)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities.
            // Disabling entity tracking makes the code a little faster.
            var queryable = _context.Balances.Where(p => p.PlayerId == balanceQuery.PlayerId).AsNoTracking();

			if (balanceQuery.Asset.HasValue && balanceQuery.Asset > 0)
			{
				queryable = queryable.Where(p => p.Asset == balanceQuery.Asset);
			}

			return await queryable.ToListAsync();
		}

        /// Function:   AddAsync
        ///
        /// Stores an instance of balance in the repository asynchronously.
        ///
        /// Parameters:
        /// balance -   The balance.
        ///
        /// Returns:    An asynchronous result.
        public async Task AddAsync(Balance balance)
        {
            await _context.Balances.AddAsync(balance);
        }

        /// Function:   FindAsync
        ///
        /// Searches for the balance by identifier asynchronously.
        ///
        /// Parameters:
        /// balance -   The balance.
        ///
        /// Returns:    An asynchronous result that yields the find.
        public async Task<Balance> FindAsync(Balance balance)
        {
            return await _context.Balances
                                 .FirstOrDefaultAsync(p => p.Asset == balance.Asset && p.PlayerId == balance.PlayerId);
        }
    }
}