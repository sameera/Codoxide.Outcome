using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class ValueOutcomeExtensions
    {
        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Action action)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) action();

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Action<T> action)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) action(outcome.Item1);

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T> func)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) return (func(), null);

            return outcome;
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<ResultType> func)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return (func(), null);

            return (default(ResultType), failure);
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T, ResultType> func)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return (func(result), null);

            return (default(ResultType), failure);
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<Task> asyncAction)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) await asyncAction();

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<Task<T>> asyncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) return (await asyncFunc(), null);

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T, Task<T>> asyncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) return (await asyncFunc(outcome.Item1), null);

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<Task<ValueTuple<T, Failure>>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) return await aysncFunc();

            return outcome;
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<Task<ValueTuple<ResultType, Failure>>> aysncFunc)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return await aysncFunc();

            return (default(ResultType), failure);
        }

        public static async Task<(T result, Failure failure)> Then<T>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T, Task<ValueTuple<T, Failure>>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.Item2 == null) return await aysncFunc(outcome.Item1);

            return outcome;
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T, Task<ValueTuple<ResultType, Failure>>> aysncFunc)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return await aysncFunc(result);

            return (default(ResultType), failure);
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<Outcome<ResultType>> fn)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return fn();

            return (default(ResultType), failure);
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T, Outcome<ResultType>> fn)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return fn(result);

            return (default(ResultType), failure);
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<Task<Outcome<ResultType>>> asyncFunc)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return await asyncFunc();

            return (default(ResultType), failure);
        }

        public static async Task<(ResultType result, Failure failure)> Then<T, ResultType>(this Task<ValueTuple<T, Failure>> asyncPromise, Func<T, Task<Outcome<ResultType>>> asyncFunc)
        {
            var (result, failure) = await asyncPromise;
            if (failure == null) return await asyncFunc(result);

            return (default(ResultType), failure);
        }
    }
}
