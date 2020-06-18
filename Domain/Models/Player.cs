using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wallet.API.Domain.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();

        public IList<Balance> Balances { get; set; } = new List<Balance>();
    }
}