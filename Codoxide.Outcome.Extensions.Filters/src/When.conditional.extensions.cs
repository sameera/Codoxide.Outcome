using Codoxide.OutcomeExtensions.Filters;
using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;
    using static Codoxide.Internals.Utility;

    public static class WhenConditionalExtensions
    {

        public static Outcome<T> When<T>(this Outcome<T> @this, bool condition)
        {
            var (result, failure) = @this;

            if (failure != null && failure is ExpectationFailure<T> expected && condition)
            {
                return new Outcome<T>(expected.ResultAtSource);
            }
            else if (failure == null && !condition)
            {
                return (result, new ExpectationFailure<T>(result));
            }

            return @this;
        }

        public static Task<Outcome<T>> When<T>(this Task<Outcome<T>> asyncOutcome, bool condition)
        {
            return Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);
                return When(@this, condition);
            });
        }
    }
}
