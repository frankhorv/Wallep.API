using System;
using System.Runtime.Serialization;

namespace Wallet.API.Domain.Services.Exceptions
{
    [Serializable]
    public class UnknownOperationTypeException : Exception
    {
        public UnknownOperationTypeException()
        {
        }

        public UnknownOperationTypeException(string message) : base(message)
        {
        }

        public UnknownOperationTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownOperationTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}