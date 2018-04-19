using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class OutcomeExtensions
    {
        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action action)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action<T> action)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) action(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<T> func)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return new Outcome<T>(func());

            return outcome;
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> func)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return new Outcome<ResultType>(func());

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> func)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return new Outcome<ResultType>(func(outcome.Result));

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task> asyncAction)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) await asyncAction();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task<T>> asyncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return new Outcome<T>(await asyncFunc());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<T>>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return await aysncFunc();

            return outcome;
        }

        public static async Task<Outcome<ReturnValue>> Then<T, ReturnValue>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ReturnValue>>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return await aysncFunc();

            return Outcome<ReturnValue>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<T>>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return await aysncFunc(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<ReturnValue>> Then<T, ReturnValue>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ReturnValue>>> aysncFunc)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful) return await aysncFunc(outcome.Result);

            return Outcome<ReturnValue>.Reject(outcome.Failure);
        }
    }
}
