using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.OutcomeInternals.Utility;

    public static class OutcomeThenTaskThenExtensions
    {

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> func) //where ResultType: class
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return Outcome.Of(func);

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Outcome<ResultType>> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func();

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ResultType>(func(outcome.ResultOrDefault()));

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Outcome<ResultType>> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func(outcome.ResultOrDefault());

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<ReturnType>> asyncFunc) 
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<T>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return outcome;
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ReturnType>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnValue>> Then<T, ReturnValue>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ReturnValue>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnValue>.Reject(outcome.FailureOrNull());
            });
        }
    }
}
