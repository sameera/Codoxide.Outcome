using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeCatchExtensions
    {
        public static Outcome<T> Catch<T>(this Outcome<T> @this, Action action)
        {
            if (!@this.IsSuccessful) action();

            return @this;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Action<Failure> action)
        {
            if (!@this.IsSuccessful) action(@this.Failure);

            return @this;
        }

        public static Outcome<ReturnType> Catch<T, ReturnType>(this Outcome<T> @this, Func<ReturnType> fn)
        {
            if (!@this.IsSuccessful) return fn();

            return Outcome<ReturnType>.Reject(@this.Failure);
        }

        public static Outcome<ReturnType> Catch<T, ReturnType>(this Outcome<T> @this, Func<Failure, ReturnType> fn)
        {
            if (!@this.IsSuccessful) return fn(@this.Failure);

            return Outcome<ReturnType>.Reject(@this.Failure);
        }


        /*
         * ***********************************************************************************
         * Sync Operations that return Outcomes
         * ***********************************************************************************
         */
        public static Outcome<ReturnType> Catch<T, ReturnType>(this Outcome<T> @this, Func<Outcome<ReturnType>> fn)
        {
            if (!@this.IsSuccessful) return fn();

            return Outcome<ReturnType>.Reject(@this.Failure);
        }

        public static Outcome<ReturnType> Catch<T, ReturnType>(this Outcome<T> @this, Func<Failure, Outcome<ReturnType>> fn)
        {
            if (!@this.IsSuccessful) return fn(@this.Failure);

            return Outcome<ReturnType>.Reject(@this.Failure);
        }

        /*
         * ***********************************************************************************
         * Async Operations
         * ***********************************************************************************
         */

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Action action)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Action<Failure> action)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) action(outcome.Failure);

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Task> action)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) await action();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task> action)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) await action(outcome.Failure);

            return outcome;
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<ReturnType> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn();

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Failure, ReturnType> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn(outcome.Failure);

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Task<ReturnType>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn();

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Failure, Task<ReturnType>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn(outcome.Failure);

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        /*
         * ***********************************************************************************
         * Async Operations that return Outcomes
         * ***********************************************************************************
         */

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Outcome<ReturnType>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn();

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Failure, Outcome<ReturnType>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn(outcome.Failure);

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Task<Outcome<ReturnType>>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn();

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Catch<T, ReturnType>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<ReturnType>>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn(outcome.Failure);

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        /*
         * ***********************************************************************************
         * Operations that return Failures
         * ***********************************************************************************
         */

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure> action)
        {
            if (!@this.IsSuccessful) action();

            return @this;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure, Failure> action)
        {
            if (!@this.IsSuccessful) action(@this.Failure);

            return @this;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure> action)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Failure> action)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) action(outcome.Failure);

            return outcome;
        }
    }
}
