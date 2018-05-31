using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    static partial class OutcomeExtensions
    {
        public static OutcomeFinalizer<T, ReturnType> Return<T, ReturnType>(this Outcome<T> outcome, Func<T, ReturnType> successHandler)
        {
            return new OutcomeFinalizer<T, ReturnType>(outcome).OnSuccess(successHandler);
        }

        [Obsolete("Use Return-Catch-Unwrap pattern.")]
        public static void EndWith<T>(this Outcome<T> outcome, Action<T> onSuccess = null, Action<Failure> onFailure = null)
        {
            if (outcome.IsSuccessful && onSuccess != null)
            {
                onSuccess(outcome.Result);
            }
            else if (!outcome.IsSuccessful && onFailure != null)
            {
                onFailure(outcome.Failure);
            }
        }

        [Obsolete("Use Return-Catch-Unwrap pattern.")]
        public static void EndWith<T>(this Outcome<T> outcome, Action onSuccess = null, Action<Failure> onFailure = null)
        {
            outcome.EndWith(r => onSuccess(), onFailure);
        }

        [Obsolete("Use Return-Catch-Unwrap pattern.")]
        public static async Task EndWith<T>(this Task<Outcome<T>> asyncOutcome, Action<T> onSuccess = null, Action<Failure> onFailure = null)
        {
            var outcome = await asyncOutcome;
            outcome.EndWith(onSuccess, onFailure);
        }

        [Obsolete("Use Return-Catch-Unwrap pattern.")]
        public static async Task<ReturnType> Return<T, ReturnType>(
                this Task<Outcome<T>> asyncOutcome,
                Func<T, ReturnType> onSuccess = null,
                Func<Failure, ReturnType> onFailure = null,
                Func<Exception, ReturnType> onException = null,
                ReturnType fallBack = default(ReturnType)
            )
        {
            var outcome = await asyncOutcome;
            return outcome.Return(onSuccess, onFailure, onException, fallBack);
        }

        [Obsolete("Use Return-Catch-Unwrap pattern.")]
        public static ReturnType Return<T, ReturnType>(
                this Outcome<T> outcome, 
                Func<T, ReturnType> onSuccess = null,
                Func<Failure, ReturnType> onFailure = null,
                Func<Exception, ReturnType> onException = null,
                ReturnType fallBack = default(ReturnType)
            )
        {
            if (outcome.IsSuccessful && onSuccess != null)
            {
                return onSuccess(outcome.Result);
            }
            else if (!outcome.IsSuccessful && outcome.Failure.Exception != null && onException != null)
            {
                return onException(outcome.Failure.Exception);
            }
            else if (!outcome.IsSuccessful && onFailure != null)
            {
                return onFailure(outcome.Failure);
            }
            else
            {
                return fallBack;
            }
        }
    }
}
