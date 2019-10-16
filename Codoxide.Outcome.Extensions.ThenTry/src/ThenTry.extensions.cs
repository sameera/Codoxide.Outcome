using System;

namespace Codoxide
{
    using System.Threading.Tasks;

    public static class OutcomeThenTryExtensions
    {
        // These functions are now just aliases to the 'Then' function which have built in exception handling as of v5.

        public static Outcome<T> ThenTry<T>(this Outcome<T> @this, Action action) => @this.Then(action);

        public static Outcome<T> ThenTry<T>(this Outcome<T> @this, Action<T> action) => @this.Then(action);

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn) => @this.Then(fn);

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> @this, Func<ResultType> fn) => @this.Then(fn);

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> @this, Func<T, ResultType> fn) => @this.Then(fn);

        public static async Task<Outcome<T>> ThenTry<T>(this Outcome<T> @this, Func<Task> action) => await @this.Then(action);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<Task<Outcome<ResultType>>> fn) => await @this.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<T, Task<Outcome<ResultType>>> fn) => await @this.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<Task<ResultType>> fn) => await @this.Then(fn);

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<T, Task<ResultType>> fn) => await @this.Then(fn);
    }
}