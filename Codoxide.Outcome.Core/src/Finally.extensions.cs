using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class FinallyExtensions
    {
        public static TResultType Finally<T, TResultType>(this Outcome<T> @this,
                                                        Func<T, TResultType> onSuccess = null,
                                                        Func<Failure, TResultType> onFailure = null)
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

        public static async Task<TResultType> Finally<T, TResultType>(this Task<Outcome<T>> @this,
                                                        Func<T, TResultType> onSuccess = null,
                                                        Func<Failure, TResultType> onFailure = null)
        {
            var outcome = await @this.ConfigureAwait(false);
            return outcome.Finally(onSuccess, onFailure);
        }

        public static async Task<TResultType> Finally<T, TResultType>(this Task<Outcome<T>> asyncOutcome,
                                                        Func<T, Task<TResultType>> onSuccess = null,
                                                        Func<Failure, Task<TResultType>> onFailure = null)
        {
            var @this = await asyncOutcome.ConfigureAwait(false);
            var (result, failure) = @this;

            try
            {
                if (@this.IsSuccessful && onSuccess != null)
                {
                    return await onSuccess(result).ConfigureAwait(false);
                }

            }
            catch (Exception e)
            {
                if (onFailure != null) return await onFailure(new Failure(e.Message, e)).ConfigureAwait(false);
                throw;
            }

            if (!@this.IsSuccessful && onFailure != null)
            {
                return await onFailure(failure).ConfigureAwait(false);
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
    }
}
