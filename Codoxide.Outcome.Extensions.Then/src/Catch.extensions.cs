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
            if (!@this.IsSuccessful) action(@this.FailureOrThrow());

            return @this;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<T> fn)
        {
            if (!@this.IsSuccessful) return fn();

            return @this;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure, T> fn)
        {
            if (!@this.IsSuccessful) return fn(@this.FailureOrThrow());

            return @this;
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
            if (!outcome.IsSuccessful) action(outcome.FailureOrNull());

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
            if (!outcome.IsSuccessful) await action(outcome.FailureOrNull());

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<T> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, T> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn(outcome.FailureOrNull());

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Task<T>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<T>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn(outcome.FailureOrNull());

            return outcome;
        }

        /*
         * ***********************************************************************************
         * Async Operations that return Async Outcomes
         * ***********************************************************************************
         */

        //public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Outcome<T>> fn)
        //{
        //    var outcome = await @this;
        //    if (!outcome.IsSuccessful) return fn();

        //    return outcome;
        //}

        //public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Outcome<T>> fn)
        //{
        //    var outcome = await @this;
        //    if (!outcome.IsSuccessful) return fn(outcome.FailureOrNull());

        //    return outcome;
        //}

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Outcome<T>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Task<Outcome<T>>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<T>>> fn)
        {
            var outcome = await @this;
            if (!outcome.IsSuccessful) return await fn(outcome.FailureOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Outcome<T>> fn)
        {
            var outcome = await @this;
            return !outcome.IsSuccessful ? fn(outcome.FailureOrThrow()) : outcome;
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
            if (!@this.IsSuccessful) action(@this.FailureOrThrow());

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
            if (!outcome.IsSuccessful) action(outcome.FailureOrThrow());

            return outcome;
        }
    }
}