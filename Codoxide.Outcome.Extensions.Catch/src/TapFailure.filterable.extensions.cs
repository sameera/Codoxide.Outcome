using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

using static Codoxide.FixedOutcomes;
using static Codoxide.OutcomeCatchExtensions;

namespace Codoxide
{
    partial class OutcomeTapFailureExtensions
    {
        public static Outcome<T> TapFailure<T>(this Outcome<T> @this, Func<Failure, bool> filter, Action action)
        {
            if (!IsCatchable(@this, filter)) return @this;

            return Try(() => {
                action();
                return ToKnownFailed(@this);
            });
        }

        public static Outcome<T> TapFailure<T>(this Outcome<T> @this, Func<Failure, bool> filter, Action<Failure> action)
        {
            if (!IsCatchable(@this, filter)) return @this;

            return Try(() => {
                action(@this.FailureOrThrow());
                return ToKnownFailed(@this);
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Failure, bool> filter, Action action)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            return Try(() => {
                action();
                return ToKnownFailed(outcome);
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Failure, bool> filter, Action<Failure> action)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            return Try(() => {
                action(outcome.FailureOrNull());
                return ToKnownFailed(outcome);
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Failure, bool> filter, Func<Task> action)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                await action(); 
                return ToKnownFailed(outcome);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }


        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Failure, bool> filter, Func<Failure, Task> action)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                await action(outcome.FailureOrNull());
                return ToKnownFailed(outcome);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}