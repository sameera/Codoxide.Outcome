using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class OutcomeExtensions
    {
        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action action)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) action();

            return promise;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action<T> action)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) action(promise.Result);

            return promise;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<T> func)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) new Outcome<T>(func());

            return promise;
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> func)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) new Outcome<ResultType>(func());

            return Outcome<ResultType>.Reject(promise.Failure);
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> func)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) new Outcome<ResultType>(func(promise.Result));

            return Outcome<ResultType>.Reject(promise.Failure);
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task> asyncAction)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) await asyncAction();

            return promise;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task<T>> asyncFunc)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) new Outcome<T>(await asyncFunc());

            return promise;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<T>>> aysncFunc)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) return await aysncFunc();

            return promise;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<T>>> aysncFunc)
        {
            var promise = await asyncPromise;
            if (promise.IsSuccessful) return await aysncFunc(promise.Result);

            return promise;
        }
    }
}
