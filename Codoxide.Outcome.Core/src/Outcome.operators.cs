using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    partial struct Outcome<T>
    {
        public static implicit operator Outcome<T>(T result) => new Outcome<T>(result);
        
        public static explicit operator T(Outcome<T> outcome) => outcome.ResultOrDefault();

        public static implicit operator Outcome<T>(Failure failure) => new Outcome<T>(failure);

        public static implicit operator Outcome<T>(Exception exception) => new Outcome<T>(Fail(exception));

        public static implicit operator Outcome<T>((T result, Failure failure) outcome) => new Outcome<T>(outcome.result, outcome.failure);

        public static implicit operator Outcome<T>((T result, Exception exception) tuple) => new Outcome<T>(tuple.result, Fail(tuple.exception));

        public static implicit operator ValueTuple<T, Failure>(Outcome<T> outcome) => (outcome._result, outcome._failure);
    }
}
