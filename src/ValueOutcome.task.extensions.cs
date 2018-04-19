using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class ValueOutcomeExtensions
    {
        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Action action)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) action();

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Action<T> action)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) action(outcome.result);

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Func<T> func)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) return (func(), null);

            return outcome;
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<(T result, Failure failure)> asyncPromise, Func<ResultType> func)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return (func(), null);

            return (default(ResultType), failure);
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<(T result, Failure failure)> asyncPromise, Func<T, ResultType> func)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return (func(result), null);

            return (default(ResultType), failure);
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Func<Task> asyncAction)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) await asyncAction();

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Func<Task<T>> asyncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) return (await asyncFunc(), null);

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Func<Task<(T result, Failure failure)>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) return await aysncFunc();

            return outcome;
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<(T result, Failure failure)> asyncPromise, Func<Task<(ResultType result, Failure failure)>> aysncFunc)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return await aysncFunc();

            return (default(ResultType), failure);
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<(T result, Failure failure)> asyncPromise, Func<T, Task<(T result, Failure failure)>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.failure == null) return await aysncFunc(outcome.result);

            return outcome;
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<(T result, Failure failure)> asyncPromise, Func<T, Task<(ResultType result, Failure failure)>> aysncFunc)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return await aysncFunc(result);

            return (default(ResultType), failure);
        }
    }
}
