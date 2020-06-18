using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Services.Exceptions;

namespace Wallet.API.Services.Commands
{
    public delegate Task operationDelegate(Player player, EAsset asset, ulong amount);

    public class OperationCommand
    {
        public operationDelegate DepositDelegate { get; set; }
        public operationDelegate StakeDelegate { get; set; }
        public operationDelegate WinDelegate { get; set; }

        public async Task ExecuteAction(Transaction transaction, Operation operation)
        {
            switch (operation.Type)
            {
                case EOperation.Deposit:
                    await DepositDelegate(transaction.Player, operation.Asset, operation.Amount);
                    break;
                case EOperation.Stake:
                    await StakeDelegate(transaction.Player, operation.Asset, operation.Amount);
                    break;
                case EOperation.Win:
                    await WinDelegate(transaction.Player, operation.Asset, operation.Amount);
                    break;
                default:
                    throw new UnknownOperationTypeException($"Invalid operation type {operation.Type}");
            }
        }
    }
}
