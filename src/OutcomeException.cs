using System;
using System.Runtime.Serialization;

namespace Codoxide.Outcomes
{
    [Serializable]
    internal class OutcomeException : Exception
    {
        public int FailureCode { get; internal set; } = Failure.GeneralFailure;

        public OutcomeException()
        {
        }

        public OutcomeException(string message) : base(message)
        {
        }

        public OutcomeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutcomeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}