using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

using static Codoxide.OutcomeThenExtensions;
using static Codoxide.FixedOutcomes;
using System.Diagnostics;

namespace Codoxide
{
    public static class OutcomeTapFailureExtensions
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

        private static bool IsIgnorable<T>(Outcome<T> @this)
        {
            var (_, failure) = @this;
            return failure == null || failure is KnownFailure;
        }

        private static Outcome<T> ToKnownFailed<T>(Outcome<T> outcome)
        {
            Debug.Assert(!IsIgnorable(outcome));
            return Outcome<T>.Reject(new KnownFailure(outcome.FailureOrThrow()));
        }
    }
}