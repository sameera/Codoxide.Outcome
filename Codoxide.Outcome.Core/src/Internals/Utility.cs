#nullable enable

using System;
using System.Threading.Tasks;

namespace Codoxide.OutcomeInternals
{
    using static Codoxide.FixedOutcomes;

    public static class Utility
    {
        public static async Task<Outcome<T>> Try<T>(Func<Task<Outcome<T>>> func)
        {
            try
            {
                return await func().ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Outcome<T>.Reject(Fail(ex));
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static Outcome<T> Try<T>(Func<Outcome<T>> func)
        {
            try
            {
                return func();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Fail(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static Outcome<T> TryAsAction<T, TAny>(Func<Outcome<TAny>> func, Outcome<T> returnValue)
        {
            try
            {
                var actionResult = func();
                return actionResult.IsSuccessful 
                    ?  returnValue
                    : Outcome<T>.Reject(actionResult.FailureOrThrow());
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Fail(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static async Task<Outcome<T>> TryAsActionAsync<T, TAny>(Func<Task<Outcome<TAny>>> func, Outcome<T> returnValue)
        {
            try
            {
                var actionResult = await func();
                return actionResult.IsSuccessful 
                    ?  returnValue
                    : Outcome<T>.Reject(actionResult.FailureOrThrow());
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Fail(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
