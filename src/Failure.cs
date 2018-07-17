using System;

namespace Codoxide.Outcomes
{
    public class Failure
    {
        public const int GeneralFailure = 500;

        public string Reason { get; }

        public Exception Exception { get; }

        public int FailureCode { get; }

        public Failure(string reason, int failureCode = GeneralFailure)
        {
            this.Reason = reason;
            this.FailureCode = failureCode;
        }

        public Failure(string reason, Exception exception, int failureCode = GeneralFailure) : this(reason, failureCode)
        {
            this.Exception = exception;
        }

        public Exception ToException()
        {
            if (this.Exception != null)
            {
                return this.Exception;
            }
            else
            {
                return new OutcomeException(this.Reason) { FailureCode = this.FailureCode };
            }
        }

        public override string ToString()
        {
            return this.Exception == null 
                ? this.Reason 
                : string.Concat(this.Reason, "\r\n", this.Exception);
        }
    }
}
