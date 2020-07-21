using Codoxide.Outcomes;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    using Filter = Func<Failure, bool>;

    public static partial class OutcomeCatchExtensions
    {
        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<T> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;

            return Outcome.Of(() => fn());
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<Failure, T> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;


            return Outcome.Of(() => fn(@this.FailureOrThrow()));
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<Outcome<T>> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<Failure, Outcome<T>> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;

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

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure, T> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            return Outcome.Of(() => fn(outcome.FailureOrNull()));
        }
        
        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Outcome<T>> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure, Outcome<T>> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Task<T>> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return await fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure, Task<T>> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return await fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Task<Outcome<T>>> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return await fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure, Task<Outcome<T>>> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

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

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<Failure> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<Failure, Failure> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;
            
            try
            {
                return fn(@this.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;
            
            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure, Failure> fn)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;
            
            try
            {
                return fn(outcome.FailureOrThrow());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        /****************************************************************************************
         * 
         * When the Catch block only wants to do an action but, doesn't want to 
         * generate a new rejected Outcome...
         * 
         * **************************************************************************************/

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Filter filter, Func<Failure, Outcome<T>, Outcome<T>> action)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return action(outcome.FailureOrThrow(), ToKnownFailed(outcome));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static async Task<Outcome<T>> Catch<T>(
                this Task<Outcome<T>> @this, Filter filter,
                Func<Failure, Outcome<T>, Task<Outcome<T>>> action)
        {
            var outcome = await @this;
            if (!IsCatchable(outcome, filter)) return outcome;

            try
            {
                return await action(outcome.FailureOrThrow(), ToKnownFailed(outcome));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        public static Outcome<T> Catch<T>(this Outcome<T> @this, Filter filter, Func<Failure, Outcome<T>, Outcome<T>> fn)
        {
            if (!IsCatchable(@this, filter)) return @this;

            try
            {
                return fn(@this.FailureOrThrow(), ToKnownFailed(@this));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        internal static bool IsCatchable<T>(Outcome<T> @this, Filter filter)
        {
            var (_, failure) = @this;

            return failure != null && filter(failure);
        }

    }
}