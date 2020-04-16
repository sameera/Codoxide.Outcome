using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

using static Codoxide.OutcomeThenExtensions;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static class OutcomeCatchExtensions
    {
        public static Outcome<T> Catch<T>(this Outcome<T> @this, Action action)
        {
            if (@this.IsSuccessful) return @this;

            return Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Action<Failure> action)
        {
            if (@this.IsSuccessful) return @this;

            return Try(() => {
                action(@this.FailureOrThrow());
                return @this;
            });
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<T> fn)
        {
            if (@this.IsSuccessful) return @this;

            return Outcome.Of(() => fn());
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure, T> fn)
        {
            if (@this.IsSuccessful) return @this;


            return Outcome.Of(() => fn(@this.FailureOrThrow()));
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Outcome<T>> fn)
        {
            if (@this.IsSuccessful) return @this;

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure, Outcome<T>> fn)
        {
            if (@this.IsSuccessful) return @this;

            try
            {
                return fn(@this.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        /*
         * ***********************************************************************************
         * Async Operations
         * ***********************************************************************************
         */

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Action action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            return Try(() => {
                action();
                return outcome;
            });
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Action<Failure> action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            return Try(() => {
                action(outcome.FailureOrNull());
                return outcome;
            });
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Task> action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                await action();
                return outcome;
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<T> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;
            
            return Outcome.Of(fn);
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, T> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            return Outcome.Of(() => fn(outcome.FailureOrNull()));
        }
        
        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Outcome<T>> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Outcome<T>> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                return fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task> action)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                await action(outcome.FailureOrNull());
                return outcome;
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Task<T>> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                return await fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<T>> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                return await fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Task<Outcome<T>>> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                return await fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<T>>> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;

            try
            {
                return await fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        /*
         * ***********************************************************************************
         * Operations that return Failures
         * ***********************************************************************************
         */

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure> fn)
        {
            if (@this.IsSuccessful) return @this;

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Func<Failure, Failure> fn)
        {
            if (@this.IsSuccessful) return @this;
            
            try
            {
                return fn(@this.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;
            
            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Failure> fn)
        {
            var outcome = await @this;
            if (outcome.IsSuccessful) return outcome;
            
            try
            {
                return fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}