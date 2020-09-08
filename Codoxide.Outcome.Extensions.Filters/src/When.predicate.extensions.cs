using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.Internals.Utility;
    using static Codoxide.OutcomeExtensions.Filters.Utility;
    using static FixedOutcomes;

    public static class WhenPredicateExtensions
    {
        public static Outcome<T> When<T>(this Outcome<T> @this, Func<bool> condition)
        {
            var (result, failure) = @this;

            if (failure != null && failure is ExpectationFailure<T> expected && condition())
            {
                return new Outcome<T>(expected.ResultAtSource);
            }
            else if (failure == null && !condition())
            {
                return (result, new ExpectationFailure<T>(result));
            }

            return @this;
        }

        public static Task<Outcome<T>> When<T>(this Task<Outcome<T>> asyncOutcome, Func<bool> condition)
        {
            return Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);
                return When(@this, condition);
            });
        }

        public static Task<Outcome<T>> When<T>(this Task<Outcome<T>> asyncOutcome, Func<Task<bool>> asyncPredicate)
        {
            return Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);
                if (IsUnprocessable(@this)) return @this;

                var condition = await asyncPredicate().ConfigureAwait(false);
                return @this.When(condition);
            });
        }
    }
}