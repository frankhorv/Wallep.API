using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Repositories;
using Wallet.API.Domain.Services;
using Wallet.API.Domain.Services.Communication;
using Wallet.API.Domain.Services.Exceptions;
using Wallet.API.Services.Commands;

namespace Wallet.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionCommand _transactionCommand;

        public TransactionService
            (
            IPlayerRepository playerRepository,
            ITransactionRepository transactionRepository,
            IBalanceRepository balanceRepository,
            IUnitOfWork unitOfWork
            )
        {
            _playerRepository = playerRepository;
            _transactionRepository = transactionRepository;
            _balanceRepository = balanceRepository;
            _unitOfWork = unitOfWork;
            var operationCommand = new OperationCommand
            {
                DepositDelegate = Deposit,
                StakeDelegate = Stake,
                WinDelegate = Win
            };
            _transactionCommand = new TransactionCommand(operationCommand);
        }

        public async Task<TransactionResponse> SaveAsync(Guid playerId, IEnumerable<Transaction> transactions)
        {
            if (transactions.Count() == 0)
            {
                return new TransactionResponse("Empty transactions");
            }

            var existingPlayer = await _playerRepository.FindByIdAsync(playerId);
            if (existingPlayer == null)
            {
                return new TransactionResponse("Player not exist");
            }

            try
            {
                TransactionStatistics transactionStatistics = new TransactionStatistics();
                
                // traverse through transactions
                foreach (var transaction in transactions)
                {
                    try
                    {
                        transaction.PlayerId = playerId;
                        transaction.Player = existingPlayer;

                        var existingTransaction = await _transactionRepository.FindByIdAsync(transaction.TransactionId);
                        if (existingTransaction == null)
                        {
                            // execute transaction
                            await _transactionCommand.ExecuteAction(transaction);

                            // create a new instance of the transaction in the repository
                            transaction.Status = ETransactionStatus.Succeed;
                            await _transactionRepository.AddAsync(transaction);

                            // update successfully processed transaction statistics
                            transactionStatistics.AppliedTransactions.Add(transaction.TransactionId);
                        }
                        else
                        {
                            // duplicated transaction => return cached result
                            if (existingTransaction.Status == ETransactionStatus.Succeed)
                            {
                                transactionStatistics.AppliedTransactions.Add(transaction.TransactionId);
                            }
                            else
                            {
                                transactionStatistics.FailedTransactions.Add(new FailedTransaction { TransactionId = transaction.TransactionId, Message = existingTransaction.Message });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // update failed transaction statistics
                        transactionStatistics.FailedTransactions.Add(new FailedTransaction { TransactionId = transaction.TransactionId, Message = ex.Message });
                        transaction.Status = ETransactionStatus.Failed;
                        transaction.Message = ex.Message;
                        await _transactionRepository.AddAsync(transaction);
                    }
                }

                // story changes
                await _unitOfWork.CompleteAsync();

                return new TransactionResponse(transactionStatistics);
            }
            catch (Exception ex)
            {
                return new TransactionResponse($"An error occurred when saving the transaction: {ex.Message}");
            }
        }
        private async Task<Balance> GetBalanceByAssetAsync(Player player, EAsset asset)
        {
            var balance = player.Balances.FirstOrDefault(p => p.Asset == asset);
            if (balance == null)
            {
                balance = new Balance()
                {
                    PlayerId = player.Id,
                    Asset = asset
                };
                await _balanceRepository.AddAsync(balance);
            }

            return balance;
        }

        private async Task Deposit(Player player, EAsset asset, ulong amount)
        {
            var balance = await GetBalanceByAssetAsync(player, asset);

            // check for value overflow
            if (ulong.MaxValue - balance.Amount >= amount)
            {
                balance.Amount += amount;
            }
            else
            {
                throw new PlayersBalanceOverflowException($"Cannot add {amount} {asset} to player balance {balance.Amount} {balance.Asset}");
            }
        }

        private async Task Stake(Player player, EAsset asset, ulong amount)
        {
            var balance = await GetBalanceByAssetAsync(player, asset);

            // check for value underflow
            if (balance.Amount >= amount)
            {
                balance.Amount -= amount;
            }
            else
            {
                throw new InsufficientPlayersBalanceException($"Cannot stake {amount} {asset} from player balance {balance.Amount} {balance.Asset}");
            }
        }

        private async Task Win(Player player, EAsset asset, ulong amount)
        {
            await Deposit(player, asset, amount);
        }
    }
}