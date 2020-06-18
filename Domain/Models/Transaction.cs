using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wallet.API.Domain.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public Guid TransactionId { get; set; }

        public IList<Operation> Operations { get; set; } = new List<Operation>();

        public Guid PlayerId { get; internal set; }

        public Player Player { get; internal set; }

        public string Message { get; internal set; }

        public ETransactionStatus Status { get; internal set; }
    }
}
