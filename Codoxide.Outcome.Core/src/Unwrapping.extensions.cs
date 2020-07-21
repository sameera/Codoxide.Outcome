using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class UnwrappingExtensions
    {
        public static async Task<T> ResultOrDefault<T>(this Task<Outcome<T>> @this)
            => (await @this).ResultOrDefault();

        public static async Task<T> ResultOrThrow<T>(this Task<Outcome<T>> @this)
            => (await @this).ResultOrThrow();

        public static async Task<Failure> FailureOrNull<T>(this Task<Outcome<T>> @this)
            => (await @this).FailureOrNull();

        public static async Task<Failure> FailureOrThrow<T>(this Task<Outcome<T>> @this)
            => (await @this).FailureOrThrow();

        public static ResultType Finally<T, ResultType>(this Outcome<T> @this, 
                                                        Func<T, ResultType> onSuccess = null, 
                                                        Func<Failure, ResultType> onFailure = null)
        {
            var (result, failure) = @this;

            try
            {
                if (@this.IsSuccessful && onSuccess != null)
                {
                    return onSuccess(result);
                }

            }
            catch (Exception e)
            {
                if (onFailure != null) return onFailure(new Failure(e.Message, e));
                throw;
            }

            if (!@this.IsSuccessful && onFailure != null)
            {
                return onFailure(failure);
            }
            else if (@this.IsSuccessful)
            {
                throw new OutcomeException($"{nameof(onSuccess)} handler not provided for the successful outcome.");
            }
            else
            {
                throw new OutcomeException($"{nameof(onFailure)} handler not provided for failed outcome.");
            }
        }

        public static async Task<ResultType> Finally<T, ResultType>(this Task<Outcome<T>> @this,
                                                        Func<T, ResultType> onSuccess = null,
                                                        Func<Failure, ResultType> onFailure = null)
        {
            var outcome = await @this;
            return outcome.Finally(onSuccess, onFailure);
        }
    }
}
