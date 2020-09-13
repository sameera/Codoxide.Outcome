using Codoxide.OutcomeInternals;
using Codoxide.OutcomeExtensions.Filters;
using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide.OutcomeExtensions.Filters
{
    using static Codoxide.OutcomeInternals.Utility;

    public struct Switchable<T>
    {
        private static readonly Outcome<T> _failedOutcome = Outcome<T>.Reject(new KnownFailure("Case expression was not met.", 101));

        public T PrecedentValue { get; }

        internal Switchable(T precedentValue)
        {
            PrecedentValue = precedentValue;
        }

        public Outcome<TResult> When<TResult>(bool condition, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return condition
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public async Task<Outcome<TResult>> When<TResult>(bool condition, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return condition
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public Outcome<TResult> When<TResult>(T match, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return Equals(PrecedentValue, match)
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public async Task<Outcome<TResult>> When<TResult>(T match, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return Equals(PrecedentValue, match)
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public Outcome<TResult> When<TResult>(object match, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return Equals(PrecedentValue, match)
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public async Task<Outcome<TResult>> When<TResult>(object match, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return Equals(PrecedentValue, match)
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public Outcome<TResult> When<TResult>(Func<T, bool> predicate, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return predicate != null && predicate(PrecedentValue)
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public async Task<Outcome<TResult>> When<TResult>(Func<T, Task<bool>> predicate, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return predicate != null && await predicate(PrecedentValue).ConfigureAwait(false)
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(PrecedentValue));
        }

        public Outcome<TResult> Otherwise<TResult>(Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return Try(() => func(precedent));
        }

        public async Task<Outcome<TResult>> Otherwise<TResult>(Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(PrecedentValue);
            return await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public static implicit operator T(Switchable<T> source) => source.PrecedentValue;
    }
}
