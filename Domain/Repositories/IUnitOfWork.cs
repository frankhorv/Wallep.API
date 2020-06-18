using System.Threading.Tasks;

namespace Wallet.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}