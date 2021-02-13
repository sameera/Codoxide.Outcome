using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.OutcomeInternals.Utility;

    public static class FilterAsyncEtensions
    {

        public static async Task<Outcome<bool>> Filter(this Task<Outcome<bool>> asyncOutcome)
        {
            return await Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);
                return FilterExtensions.Filter(@this);
            })
                .ConfigureAwait(false);
        }

        public static Task<Outcome<bool>> When(this Task<Outcome<bool>> @this) => Filter(@this);
        
        public static async Task<Outcome<bool>> Otherwise(this Task<Outcome<bool>> asyncCondition)
        {

            var @this = await asyncCondition.ConfigureAwait(false);
            if (@this.IsSuccessful && @this.ResultOrThrow())
            {
                return (false, new ExpectationFailure<bool>(false));
            }
            return @this;
        }

        public static async Task<Outcome<T>> Filter<T>(this Task<Outcome<T>> asyncOutcome, Func<T, bool> predicate)
        {
            return await Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);
                return FilterExtensions.Filter(@this, predicate);
            })
            .ConfigureAwait(false);
        }

    }
}