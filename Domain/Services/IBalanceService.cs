using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Models.Queries;
using Wallet.API.Domain.Services.Communication;

namespace Wallet.API.Domain.Services
{
    public interface IBalanceService
    {
        Task<IEnumerable<Balance>> ListAsync(BalanceQuery query);
        Task<BalanceResponse> SaveAsync(Balance balance);
    }
}
