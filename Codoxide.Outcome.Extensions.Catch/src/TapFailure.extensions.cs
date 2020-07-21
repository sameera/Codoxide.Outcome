using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

using static Codoxide.FixedOutcomes;
using static Codoxide.OutcomeCatchExtensions;

using System.Diagnostics;

namespace Codoxide
{
    public static partial class OutcomeTapFailureExtensions
    {
        public static Outcome<T> TapFailure<T>(this Outcome<T> @this, Action action)
        {
            if (IsIgnorable(@this)) return @this;

            return Try(() => {
                action();
                return ToKnownFailed(@this);
            });
        }

        public static Outcome<T> TapFailure<T>(this Outcome<T> @this, Action<Failure> action)
        {
            if (IsIgnorable(@this)) return @this;

            return Try(() => {
                action(@this.FailureOrThrow());
                return ToKnownFailed(@this);
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Action action)
        {
            var outcome = await @this;
            if (IsIgnorable(outcome)) return outcome;

            return Try(() => {
                action();
                return ToKnownFailed(outcome);
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Action<Failure> action)
        {
            var outcome = await @this;
            if (IsIgnorable(outcome)) return outcome;

            return Try(() => {
                action(outcome.FailureOrNull());
                return ToKnownFailed(outcome);
            });
        }

        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Task> action)
        {
            var outcome = await @this;
            if (IsIgnorable(outcome)) return outcome;

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


        public static async Task<Outcome<T>> TapFailure<T>(this Task<Outcome<T>> @this, Func<Failure, Task> action)
        {
            var outcome = await @this;
            if (IsIgnorable(outcome)) return outcome;

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


        internal static Outcome<T> Try<T>(Func<Outcome<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}