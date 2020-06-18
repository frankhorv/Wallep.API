using System;
using System.Runtime.Serialization;

namespace Wallet.API.Services
{
    [Serializable]
    internal class PlayersBalanceOverflowException : Exception
    {
        public PlayersBalanceOverflowException()
        {
        }

        public PlayersBalanceOverflowException(string message) : base(message)
        {
        }

        public PlayersBalanceOverflowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayersBalanceOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}