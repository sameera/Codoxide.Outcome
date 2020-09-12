using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.Internals.Utility;

    public static class OutcomeTaskMapExtensions
    {

        public static Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> func) //where ResultType: class
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return Outcome.Of(func);

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Outcome<ResultType>> func)
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func();

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> func)
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ResultType>(func(outcome.ResultOrDefault()));

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ResultType>> Map<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Outcome<ResultType>> func)
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func(outcome.ResultOrDefault());

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<ReturnType>> asyncFunc)
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<ReturnType>> asyncFunc) 
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ReturnType>>> aysncFunc)
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static Task<Outcome<ReturnValue>> Map<T, ReturnValue>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ReturnValue>>> aysncFunc)
        {
            return Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnValue>.Reject(outcome.FailureOrNull());
            });
        }
    }
}
