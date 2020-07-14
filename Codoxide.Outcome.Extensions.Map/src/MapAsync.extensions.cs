using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;
    public static class OutcomeAsyncExtensions
    {
        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<Task<Outcome<ReturnType>>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return await asyncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<Outcome<ReturnType>>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return await asyncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        private static async Task<Outcome<T>> Try<T>(Func<Task<Outcome<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return Outcome<T>.Reject(Fail(ex));
            }
        }
    }
}