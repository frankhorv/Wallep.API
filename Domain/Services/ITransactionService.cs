using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Services.Communication;

namespace Wallet.API.Domain.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponse> SaveAsync(Guid playerId, IEnumerable<Transaction> transactions);
    }
}
