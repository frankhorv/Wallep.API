using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Models.Queries;
using Wallet.API.Domain.Repositories;
using Wallet.API.Domain.Services;
using Wallet.API.Domain.Services.Communication;

namespace Wallet.API.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BalanceService
            (
            IPlayerRepository playerRepository,
            IBalanceRepository balanceRepository,
            IUnitOfWork unitOfWork
            )
        {
            _playerRepository = playerRepository;
            _balanceRepository = balanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Balance>> ListAsync(BalanceQuery query)
        {
            return await _balanceRepository.ListAsync(query);
        }

        public async Task<BalanceResponse> SaveAsync(Balance balance)
        {
            var existingWallet = await _playerRepository.FindByIdAsync(balance.PlayerId);
            if (existingWallet == null)
            {
                await _playerRepository.AddAsync(new Player() { Id = balance.PlayerId });
                await _unitOfWork.CompleteAsync();
            }

            var existingBalance = await _balanceRepository.FindAsync(balance);
            if (existingBalance != null)
            {
                return new BalanceResponse("Asset already exist.");
            }

            try
            {
                await _balanceRepository.AddAsync(balance);
                await _unitOfWork.CompleteAsync();

                return new BalanceResponse(balance);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BalanceResponse($"An error occurred when saving the asset: {ex.Message}");
            }
        }
    }
}