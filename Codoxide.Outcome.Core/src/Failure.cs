using System;

namespace Codoxide.Outcomes
{
    public class Failure
    {
        public const int GeneralFailure = 500;

        private readonly Exception _exception;

        public string Reason { get; }
        
        [Obsolete("Use 'AsException()' instead.")]
        public Exception Exception => this.AsException();

        public int FailureCode { get; }

        public Failure(in string reason, int failureCode = GeneralFailure)
        {
            this.Reason = reason;
            this.FailureCode = failureCode;

            _exception = new OutcomeException(this.Reason) { FailureCode = this.FailureCode };
        }

        public Failure(in string reason, Exception exception, int failureCode = GeneralFailure) : this(reason, failureCode)
        {
            _exception = exception;
        }

        [Obsolete("Use 'AsException()' instead.")]
        public Exception ToException() => this.AsException();

        public Exception AsException() => _exception;

        public override string ToString()
        {
            return _exception == null 
                ? this.Reason 
                : string.Concat(this.Reason, "\r\n", _exception);
        }
    }
}
