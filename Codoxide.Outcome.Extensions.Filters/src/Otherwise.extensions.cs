using Codoxide.Internals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codoxide.OutcomeExtensions.Filters
{
    public static class OtherwiseExtensions
    {
        public static Outcome<bool> Otherwise(this Outcome<bool> @this)
        {
            if (@this.IsSuccessful && @this.ResultOrDefault()) return (true, new ExpectationFailure<bool>(true));

            return @this;
        }

        public static Outcome<T> Otherwise<T>(this Outcome<T> @this)
        {
            var (result, failure) = @this;

            if (failure is ExpectationFailure<T> expected)
            {
                return new Outcome<T>(expected.ResultAtSource);
            }
            else if (failure != null)
            {
                return @this;
            }
            else
            {
                return (result, new ExpectationFailure<T>(result));
            }
        }
    }
}
