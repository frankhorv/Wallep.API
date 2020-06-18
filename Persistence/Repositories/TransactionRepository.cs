using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Repositories;
using Wallet.API.Persistence.Contexts;

namespace Wallet.API.Persistence.Repositories
{
    /// Class:  TransactionRepository
    ///
    /// A transaction repository.
	public class TransactionRepository : BaseRepository, ITransactionRepository
	{
		public TransactionRepository(AppDbContext context) : base(context) { }

        /// Function:   FindByIdAsync
        ///
        /// Searches for the first identifier asynchronously.
        ///
        /// Parameters:
        /// id -    The transaction identifier.
        ///
        /// Returns:    An asynchronous result that yields the find by identifier.
		public async Task<Transaction> FindByIdAsync(Guid? id)
		{
			return await _context.Transactions
									.Include(p => p.Player)
									.FirstOrDefaultAsync(p => p.TransactionId == id);
		}

        /// Function:   AddAsync
        ///
        /// Stores an instance of transaction in the repository asynchronously.
        ///
        /// Parameters:
        /// transaction -   The transaction.
        ///
        /// Returns:    An asynchronous result.
		public async Task AddAsync(Transaction transaction)
		{
			await _context.Transactions.AddAsync(transaction);
		}
    }
}