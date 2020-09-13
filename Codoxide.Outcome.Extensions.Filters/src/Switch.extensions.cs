using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static OutcomeInternals.Utility;

    public static class SwitchExtensions
    {
        public static Outcome<TResult> Switch<T, TResult>(this Outcome<T> @this, params Func<Switchable<T>, (bool, Func<Outcome<TResult>>)>[] branches)
        {
            if (!@this.IsSuccessful) return Outcome<TResult>.Reject(@this.FailureOrThrow());

            var switchable = new Switchable<T>(@this.ResultOrThrow());

            return Try(() => {
                foreach (var branch in branches)
                {
                    var (canExecute, handler) = branch(switchable);
                    if (canExecute) return handler();
                }
                return new ExpectationFailure<T>(default);
            });

        }

        public static async Task<Outcome<TResult>> Switch<T, TResult>(
            this Task<Outcome<T>> @this, 
            params Func<Switchable<T>, (bool, Func<Outcome<TResult>>)>[] branches)
        {
            return await Try(async () => Switch(await @this.ConfigureAwait(false), branches)).ConfigureAwait(false);
        }
        
        public static async Task<Outcome<TResult>> Switch<T, TResult>(
            this Task<Outcome<T>> asyncOutcome, 
            params Func<Switchable<T>, (bool, Func<Task<Outcome<TResult>>>)>[] branches)
        {
            return await Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);

                if (!@this.IsSuccessful) return Outcome<TResult>.Reject(@this.FailureOrThrow());

                var switchable = new Switchable<T>(@this.ResultOrThrow());

                for (int i = 0; i < branches.Length; i++)
                {
                    var (canExecute, handler) = branches[i](switchable);
                    if (canExecute) return await handler().ConfigureAwait(false);
                }
                
                return Outcome<TResult>
                    .Reject(new ExpectationFailure<T>(default));
            }).ConfigureAwait(false);
        }
    }
}