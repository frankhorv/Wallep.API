using System;
using System.ComponentModel.DataAnnotations;
using Wallet.API.Domain.Models;

namespace Wallet.API.Resources
{
    public class CreateWalletResource
    {
        [Required]
        public Guid PlayerId { get; set; }

        public EAsset Asset { get; set; } // Todo: add AutoMapper to cast it to the respective enum value
    }
}