using System;
using System.ComponentModel.DataAnnotations;

namespace Wallet.API.Domain.Models
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }

        public EOperation Type { get; set; }

        public EAsset Asset { get; set; }

        public ulong Amount { get; set; }

        public DateTime DateTime { get; set; }

        public Guid TransactionId { get; set; }

        public Transaction Transaction { get; set; }
    }
}
