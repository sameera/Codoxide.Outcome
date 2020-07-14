using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

using static Codoxide.OutcomeThenExtensions;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static class OutcomeTapFailureExtensions
    {
        public static Outcome<T> TapFailure<T>(this Outcome<T> @this, Action action)
        {
            if (@this.IsSuccessful) return @this;

            return Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> TapFailure<T>(this Outcome<T> @this, Action<Failure> action)
        {
            if (@this.IsSuccessful) return @this;

            return Try(() => {
                action(@this.FailureOrThrow());
                return @this;
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Action action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            return Try(() => {
                action();
                return outcome;
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Action<Failure> action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            return Try(() => {
                action(outcome.FailureOrNull());
                return outcome;
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Task> action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                await action();
                return outcome;
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<T> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            return Outcome.Of(fn);
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Failure, Task> action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                await action(outcome.FailureOrNull());
                return outcome;
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}