using Codoxide.Internals;
using Codoxide.OutcomeExtensions.Filters;
using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide.OutcomeExtensions.Filters
{
    using static Codoxide.Internals.Utility;

    public struct Switchable<T>
    {
        private static readonly Outcome<T> _failedOutcome = Outcome<T>.Reject(new KnownFailure("Case expression was not met.", 101));

        public T Result { get; }

        internal Switchable(T result)
        {
            Result = result;
        }

        public Outcome<TResult> When<TResult>(bool condition, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return condition
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public async Task<Outcome<TResult>> When<TResult>(bool condition, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return condition
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public Outcome<TResult> When<TResult>(T match, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return Equals(Result, match)
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public async Task<Outcome<TResult>> When<TResult>(T match, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return Equals(Result, match)
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public Outcome<TResult> When<TResult>(object match, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return Equals(Result, match)
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public async Task<Outcome<TResult>> When<TResult>(object match, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return Equals(Result, match)
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public Outcome<TResult> When<TResult>(Func<T, bool> predicate, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return predicate != null && predicate(Result)
                ? Try(() => func(precedent))
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public async Task<Outcome<TResult>> When<TResult>(Func<T, Task<bool>> predicate, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return predicate != null && await predicate(Result).ConfigureAwait(false)
                ? await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false)
                : Outcome<TResult>.Reject(new ExpectationFailure<T>(Result));
        }

        public Outcome<TResult> Otherwise<TResult>(Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return Try(() => func(precedent));
        }

        public async Task<Outcome<TResult>> Otherwise<TResult>(Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return await Try(async () => await func(precedent).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public static implicit operator T(Switchable<T> source) => source.Result;
    }
}
