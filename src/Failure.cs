using System;

namespace Codoxide.Outcomes
{
    public class Failure
    {
        public string Reason { get; }

        public Exception Exception { get; }

        public Failure(string reason)
        {
            this.Reason = reason;
        }

        public Failure(string reason, Exception exception) : this(reason)
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
                return new OutcomeException(this.Reason);
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
