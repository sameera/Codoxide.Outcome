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

        internal Outcome(Failure failure) : this(default(T), failure) { }

        internal Outcome(T result, Failure failure)
        {
            Result = result;
            Failure = failure;
        }

        public Failure FailureOrDefault() => this.IsSuccessful ? null : Failure;

        public T ResultOrDefault() => this.IsSuccessful ? Result : default(T);

        public void Deconstruct(out T result, out Failure failure)
        {
            failure = this.FailureOrDefault();
            result = this.ResultOrDefault();
        }

        public static Outcome<T> Reject(string reason) => new Outcome<T>(new Failure(reason));

        public static Outcome<T> Reject(string reason, Exception exception) => new Outcome<T>(new Failure(reason, exception));

        internal static Outcome<T> Reject(Failure failure) => new Outcome<T>(default(T), failure);

        public static implicit operator Outcome<T>(T result) => new Outcome<T>(result);

        public static implicit operator Outcome<T>(Failure failure) => new Outcome<T>(failure);

        public static implicit operator Outcome<T>(Exception exception) => new Outcome<T>(Fail(exception));

        public static implicit operator Outcome<T>((T result, Failure failure) outcome) => new Outcome<T>(outcome.result, outcome.failure);

        public static implicit operator Outcome<T>((T result, Exception exception) tuple) => new Outcome<T>(tuple.result, Fail(tuple.exception));

        public static implicit operator ValueTuple<T, Failure>(Outcome<T> outcome) => (outcome.ResultOrDefault(), outcome.FailureOrDefault());
    }
}
