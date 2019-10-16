using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    public partial struct Outcome<T>
    {
        public bool IsSuccessful { get { return Failure == null; } }

        internal readonly Failure Failure;

        internal readonly T Result;

        public Outcome(T result) : this(result, null) { }

        internal Outcome(Failure failure) : this(default, failure) { }

        internal Outcome(T result, Failure failure)
        {
            Result = result;
            Failure = failure;
        }

        public Outcome(Func<T> fn)
        {
            try
            {
                Result = fn();
                Failure = null;
            }
            catch (Exception ex)
            {
                Result = default;
                Failure = Fail(ex);
            }
        }

        [Obsolete("Use 'FailureOrNull()'")]
        public Failure FailureOrDefault() => this.FailureOrNull();

        public Failure FailureOrNull() => this.Failure;

        public Failure FailureOrThrow() => this.Failure ?? throw new InvalidOperationException("There is no failure as this is a successful Outcome.");

        public T ResultOrDefault() => this.Result;

        public T ResultOrThrow() => this.IsSuccessful ? this.Result : throw new InvalidOperationException("There is no result as this is a failed outcome.");

        [Obsolete("Use 'FailureOrNull()' and then AsException()")]
        public Exception ExceptionOrDefault() => Failure?.AsException();

        public void Deconstruct(out T result, out Failure failure)
        {
            failure = this.FailureOrNull();
            result = this.ResultOrDefault();
        }

        public static Outcome<T> Reject(string reason) => new Outcome<T>(new Failure(reason));

        public static Outcome<T> Reject(string reason, Exception exception) => new Outcome<T>(new Failure(reason, exception));

        public static Outcome<T> Reject(Failure failure) => new Outcome<T>(default, failure);

        public static implicit operator Outcome<T>(T result) => new Outcome<T>(result);

        public static explicit operator T(Outcome<T> outcome) => outcome.ResultOrDefault();

        public static implicit operator Outcome<T>(Failure failure) => new Outcome<T>(failure);

        public static implicit operator Outcome<T>(Exception exception) => new Outcome<T>(Fail(exception));

        public static implicit operator Outcome<T>((T result, Failure failure) outcome) => new Outcome<T>(outcome.result, outcome.failure);

        public static implicit operator Outcome<T>((T result, Exception exception) tuple) => new Outcome<T>(tuple.result, Fail(tuple.exception));

        public static implicit operator ValueTuple<T, Failure>(Outcome<T> outcome) => (outcome.Result, outcome.Failure);

    }
}
