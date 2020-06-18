using System;
using System.Threading.Tasks;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Repositories;
using Wallet.API.Domain.Services;
using Wallet.API.Domain.Services.Communication;

namespace Wallet.API.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlayerResponse> SaveAsync(Player player)
        {
            var existingWallet = await _playerRepository.FindByIdAsync(player.Id);
            if (existingWallet != null)
            {
                return new PlayerResponse("Player already exists.");
            }

            try
            {
                await _playerRepository.AddAsync(player);
                await _unitOfWork.CompleteAsync();

                return new PlayerResponse(player);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PlayerResponse($"An error occurred when saving the player: {ex.Message}");
            }
        }
    }
}