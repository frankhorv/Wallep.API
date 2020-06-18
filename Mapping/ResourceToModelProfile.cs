using AutoMapper;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Models.Queries;
using Wallet.API.Resources;

namespace Wallet.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CreateWalletResource, Balance>();

            CreateMap<TransactionResource, Transaction>();

            CreateMap<OperationResource, Operation>();
            
            CreateMap<BalanceResource, Balance>();
            
            CreateMap<PlayersBalanceQueryResource, BalanceQuery>();
        }
    }
}