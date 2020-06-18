using System;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;

namespace Wallet.API.Services.Commands
{
    public delegate void canExecuteDelegate(Player player, Balance value);

    public class TransactionCommand
    {
        private readonly OperationCommand _operationCommand;

        public TransactionCommand(OperationCommand operationCommand)
        {
            _operationCommand = operationCommand;
        }

        public async Task ExecuteAction(Transaction transaction)
        {
            foreach (var operation in transaction.Operations)
            {
                await _operationCommand.ExecuteAction(transaction, operation);
            }
        }
    }
}
