using System;

namespace Codoxide
{
    using System.Threading.Tasks;
    using static FixedOutcomes;

    public static class OutcomeThenTryTaskExtensions
    {
        public static async Task<Outcome<T>> ThenTry<T>(this Task<Outcome<T>> asyncPromise, Action action) => await asyncPromise.Then(action);

        public static async Task<Outcome<T>> ThenTry<T>(this Task<Outcome<T>> asyncPromise, Action<T> action) => await asyncPromise.Then(action);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Outcome<ResultType>> fn) => await asyncPromise.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> fn) => await asyncPromise.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> fn) => await asyncPromise.Then(fn);

        public static async Task<Outcome<T>> ThenTry<T>(this Task<Outcome<T>> asyncPromise, Func<Task> action) => await asyncPromise.Then(action);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ResultType>>> fn) => await asyncPromise.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ResultType>>> fn) => await asyncPromise.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<ResultType>> fn) => await asyncPromise.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Task<ResultType>> fn) => await asyncPromise.Then(fn);
    }
}
