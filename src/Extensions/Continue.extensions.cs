using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeContinueExtensions
    {
        public static Outcome<ReturnType> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<ReturnType> fn) => fn();

        public static Outcome<ReturnType> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<T, Failure, ReturnType> fn) => fn(@this.Result, @this.Failure);

        public static Outcome<ReturnType> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<Outcome<ReturnType>> fn) => fn();

        public static Outcome<ReturnType> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<T, Failure, Outcome<ReturnType>> fn) => fn(@this.Result, @this.Failure);

        /*
         * ***********************************************************************************
         * Async Operations
         * ***********************************************************************************
         */

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<ReturnType> fn)
        {
            await @this.ConfigureAwait(false);
            return fn();
        }

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<T, Failure, ReturnType> fn)
        {
            var (result, failure) = await @this.ConfigureAwait(false);
            return fn(result, failure);
        }

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<Task<ReturnType>> fn)
        {
            var _ = await @this;
            return await fn().ConfigureAwait(false);
        }

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<T, Failure, Task<ReturnType>> fn)
        {
            var (result, failure) = await @this;
            return await fn(result, failure);
        }

        /*
         * ***********************************************************************************
         * Async Operations that return Outcomes
         * ***********************************************************************************
         */

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<Outcome<ReturnType>> fn)
        {
            await @this.ConfigureAwait(false);
            return fn();
        }

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<T, Failure, Outcome<ReturnType>> fn)
        {
            var (result, failure) = await @this.ConfigureAwait(false);
            return fn(result, failure);
        }

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<Task<Outcome<ReturnType>>> fn)
        {
            var _ = await @this;
            return await fn().ConfigureAwait(false);
        }

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>(this Task<Outcome<T>> @this, Func<T, Failure, Task<Outcome<ReturnType>>> fn)
        {
            var (result, failure) = await @this;
            return await fn(result, failure);
        }

        /*
         * ***********************************************************************************
         * Sync to Async Conversion Operations
         * ***********************************************************************************
         */
        
        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<Task<ReturnType>> fn) => await fn().ConfigureAwait(false);

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<T, Failure, Task<ReturnType>> fn) => await fn(@this.Result, @this.Failure);

        /*
         * ***********************************************************************************
         * Sync to Async Conversion Operations that return Outcomes
         * ***********************************************************************************
         */

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<Task<Outcome<ReturnType>>> fn) => await fn().ConfigureAwait(false);

        public static async Task<Outcome<ReturnType>> Continue<T, ReturnType>
            (this Outcome<T> @this, Func<T, Failure, Task<Outcome<ReturnType>>> fn) => await fn(@this.Result, @this.Failure);
    }
}
