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
        
        /// <summary>
        /// Filters through outcomes that match the given value and marks others with an <see cref="Codoxide.OutcomeExtensions.Filters.ExpectaionFailure"/>.
        /// </summary>
        /// <typeparam name="T">The type of the precedent Outcome.</typeparam>
        /// <param name="this">The precedent.</param>
        /// <param name="matchValue">The expected value.</param>
        /// <returns></returns>
        /// <remarks>This method will cause boxing of value types.</remarks>
        public static async Task<Outcome<T>> Filter<T>(this Task<Outcome<T>> asyncOutcome, T matchValue)
        {
            return await Try(async () => {
                var @this = await asyncOutcome;
                return FilterExtensions.Filter(@this, matchValue);
            })
                .ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> Filter<T>(this Task<Outcome<T>> asyncOutcome, T matchValue, IComparer<T> comparer)
        {
            return await Try(async () => {
                var @this = await asyncOutcome;
                return FilterExtensions.Filter(@this, matchValue, comparer);
            })
                .ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> Filter<T>(this Task<Outcome<T>> asyncOutcome, Func<T, bool> predicate)
        {
            return await Try(async () => {
                var @this = await asyncOutcome;
                return FilterExtensions.Filter(@this, predicate);
            })
            .ConfigureAwait(false);
        }

    }
}