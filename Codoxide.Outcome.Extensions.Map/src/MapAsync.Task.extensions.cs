using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;

    public static class OutcomeTaskMapExtensions
    {

        public static async Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> func) //where ResultType: class
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return Outcome.Of(func);

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Outcome<ResultType>> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func();

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ResultType>(func(outcome.ResultOrDefault()));

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Outcome<ResultType>> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func(outcome.ResultOrDefault());

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<ReturnType>> asyncFunc) 
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<T>> Map<T>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<T>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return outcome;
            });
        }

        public static async Task<Outcome<ReturnType>> Map<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ReturnType>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnValue>> Map<T, ReturnValue>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ReturnValue>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnValue>.Reject(outcome.FailureOrNull());
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
