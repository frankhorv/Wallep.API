using System;
using System.Runtime.Serialization;

namespace Wallet.API.Domain.Services.Exceptions
{
    [Serializable]
    public class InsufficientPlayersBalanceException : Exception
    {
        public InsufficientPlayersBalanceException()
        {
        }

        public InsufficientPlayersBalanceException(string message) : base(message)
        {
        }

        public InsufficientPlayersBalanceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientPlayersBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}