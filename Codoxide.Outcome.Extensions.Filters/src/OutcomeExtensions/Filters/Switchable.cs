using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide.OutcomeExtensions.Filters
{
    public class Switchable<T>
    {
        public T Value { get; }

        internal Switchable(T outcome)
        {
            Value = outcome;
        }

        public Outcome<T> When(bool condition)
            => condition
                ? new Outcome<T>(Value)
                : Outcome<T>.Reject(new ExpectationFailure<T>(Value));

        public Outcome<T> When(T match)
            => Equals(Value, match)
                ? new Outcome<T>(Value)
                : Outcome<T>.Reject(new ExpectationFailure<T>(Value));

        public Outcome<T> When(object match)
            => Equals(Value, match)
                ? new Outcome<T>(Value)
                : Outcome<T>.Reject(new ExpectationFailure<T>(Value));

        public Outcome<T> When(Func<T, bool> predicate)
            => predicate != null && predicate(Value)
                ? new Outcome<T>(Value)
                : Outcome<T>.Reject(new ExpectationFailure<T>(Value));

        public async Task<Outcome<T>> When(Func<T, Task<bool>> predicate)
            => predicate != null && await predicate(Value).ConfigureAwait(false)
                ? new Outcome<T>(Value)
                : Outcome<T>.Reject(new ExpectationFailure<T>(Value));

        public Outcome<T> Otherwise() => new Outcome<T>(Value);

        public static implicit operator T(Switchable<T> source) => source.Value;
    }
}
