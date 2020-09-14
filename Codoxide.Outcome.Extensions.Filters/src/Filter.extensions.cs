using System;
using Codoxide.OutcomeExtensions.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.OutcomeExtensions.Filters.Utility;
    using static Codoxide.OutcomeInternals.Utility;

    public static class FilterExtensions
    {
        
        public static Outcome<bool> Filter(this Outcome<bool> @this)
        {
            if (@this.IsSuccessful && !@this.ResultOrThrow())
            {
                return (false, new ExpectationFailure<bool>(false));
            }
            return @this;
        }

        public static Outcome<bool> When(this Outcome<bool> @this) => Filter(@this);
        
        public static Outcome<bool> Otherwise(this Outcome<bool> @this)
        {
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
        public static Outcome<T> Filter<T>(this Outcome<T> @this, T matchValue)
        {
            if (IsUnprocessable(@this)) return @this;

            T result = @this.IsSuccessful ? @this.ResultOrThrow() : (@this.FailureOrThrow() as ExpectationFailure<T>).ResultAtSource;
            if (!Equals(result, matchValue))
            {
                return new ExpectationFailure<T>(result);
            }

            return @this;
        }

        public static Outcome<T> Filter<T>(this Outcome<T> @this, T matchValue, IComparer<T> comparer)
        {
            if (IsUnprocessable(@this)) return @this;

            T result = @this.IsSuccessful ? @this.ResultOrThrow() : (@this.FailureOrThrow() as ExpectationFailure<T>).ResultAtSource;
            if (comparer.Compare(result, matchValue) != 0)
            {
                return new ExpectationFailure<T>(result);
            }

            return @this;
        }

        public static Outcome<T> Filter<T>(this Outcome<T> @this, Func<T, bool> predicate)
        {
            if (IsUnprocessable(@this)) return @this;

            T result = @this.IsSuccessful ? @this.ResultOrThrow() : (@this.FailureOrThrow() as ExpectationFailure<T>).ResultAtSource;
            return Try(() => {
                if (!predicate(result)) return new ExpectationFailure<T>(result);

                return @this;
            });
        }

    }
}