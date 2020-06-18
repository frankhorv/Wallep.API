using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Models.Queries;

namespace Wallet.API.Domain.Repositories
{
    public interface IBalanceRepository
    {
        Task<IEnumerable<Balance>> ListAsync(BalanceQuery query);
        Task AddAsync(Balance balance);
        Task<Balance> FindAsync(Balance balance);
    }
}
