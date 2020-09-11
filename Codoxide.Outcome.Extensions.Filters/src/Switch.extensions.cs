using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Internals.Utility;

    public static class SwitchExtensions
    {
        public static Outcome<TResult> Switch<T, TResult>(this Outcome<T> @this, params Func<Switchable<T>, Outcome<TResult>>[] branches)
        {
            if (!@this.IsSuccessful) return Outcome<TResult>.Reject(@this.FailureOrThrow());

            var switchable = new Switchable<T>(@this.ResultOrThrow());

            for (int i = 0; i < branches.Length; i++)
            {
                var outcome = branches[i](switchable);
                if (outcome.IsSuccessful) return outcome;
            }

            return new ExpectationFailure<T>(default);
        }

        public static async Task<Outcome<TResult>> Switch<T, TResult>(this Task<Outcome<T>> @this, params Func<Switchable<T>, Outcome<TResult>>[] branches)
        {
            return await Try(async () => Switch(await @this.ConfigureAwait(false), branches)).ConfigureAwait(false);
        }
        public static async Task<Outcome<TResult>> Switch<T, TResult>(this Task<Outcome<T>> asyncOutcome, params Func<Switchable<T>, Task<Outcome<TResult>>>[] branches)
        {
            return await Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);

                if (!@this.IsSuccessful) return Outcome<TResult>.Reject(@this.FailureOrThrow());

                var switchable = new Switchable<T>(@this.ResultOrThrow());

                for (int i = 0; i < branches.Length; i++)
                {
                    var outcome = await branches[i](switchable).ConfigureAwait(false);
                    if (outcome.IsSuccessful) return outcome;
                }

                return new ExpectationFailure<T>(default);
            }).ConfigureAwait(false);
        }
    }
}