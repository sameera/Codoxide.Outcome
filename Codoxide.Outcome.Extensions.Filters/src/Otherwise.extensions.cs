using System;
using System.Threading.Tasks;

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

        public static async Task<Outcome<T>> Otherwise<T>(this Task<Outcome<T>> @this)
        {
            try
            {
                var outcome = await @this;

                var (result, failure) = outcome;

                if (failure is ExpectationFailure<T> expected)
                {
                    return new Outcome<T>(expected.ResultAtSource);
                }
                else if (failure != null)
                {
                    return outcome;
                }
                else
                {
                    return (result, new ExpectationFailure<T>(result));
                }
            }
            catch (Exception ex)
            {
                return Outcome<T>.Reject(ex);
            }
        }
    }
}