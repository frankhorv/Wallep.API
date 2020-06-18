using AutoMapper;
using Wallet.API.Domain.Models;
using Wallet.API.Resources;

namespace Wallet.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Player, WalletResource>();

            CreateMap<Transaction, TransactionResource>();

            CreateMap<Balance, BalanceResource>();
        }
    }
}