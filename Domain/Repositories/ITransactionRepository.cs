using System;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;

namespace Wallet.API.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction player);
        Task<Transaction> FindByIdAsync(Guid? id);
    }
}