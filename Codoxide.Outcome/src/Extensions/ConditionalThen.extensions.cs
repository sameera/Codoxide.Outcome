using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeConditionalThenExtensions
    {
        public static Outcome<T> Then<T>(this Outcome<T> outcome, bool condition, Action action)
        {
            if (outcome.IsSuccessful && condition) action();

            return outcome;
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, bool condition, Action<T> action)
        {
            if (outcome.IsSuccessful && condition) action(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, bool condition, Func<Task> action)
        {
            if (outcome.IsSuccessful && condition) await action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, bool condition, Func<T, Task> action)
        {
            if (outcome.IsSuccessful && condition) await action(outcome.ResultOrThrow());

            return outcome;
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, bool condition, Func<T> fn)
        {
            if (outcome.IsSuccessful && condition) return fn();

            return Outcome<T>.Reject(outcome.FailureOrThrow());
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, bool condition, Func<Outcome<T>> fn)
        {
            if (outcome.IsSuccessful && condition) return fn();

            return Outcome<T>.Reject(outcome.FailureOrThrow());
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, bool condition, Func<Failure> fn)
        {
            if (outcome.IsSuccessful && condition) return fn();

            return Outcome<T>.Reject(outcome.FailureOrThrow());
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, bool condition, Func<ValueTuple<T, Failure>> fn)
        {
            if (outcome.IsSuccessful && condition) return (Outcome<T>)fn();

            return Outcome<T>.Reject(outcome.FailureOrThrow());
        }

        //public static Outcome<T> When<T, OutType>(this Outcome<T> outcome, bool condition, out OutType output, OutFunc<OutType, Outcome<T>> fn)
        //{
        //    if (outcome.IsSuccessful && condition)
        //    {
        //        return fn(out output);
        //    }
        //    else
        //    {
        //        output = default(OutType);
        //        return outcome;
        //    }
        //}

        //public static Outcome<T> When<T, OutType>(this Outcome<T> outcome, bool condition, out OutType output, ParameterziedOutFunc<T, OutType, Outcome<T>> fn)
        //{
        //    if (outcome.IsSuccessful && condition)
        //    {
        //        return fn(outcome.ResultOrThrow(), out output);
        //    }
        //    else
        //    {
        //        output = default(OutType);
        //        return Outcome<T>.Reject(outcome.FailureOrThrow());
        //    }
        //}

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action action)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action<T> action)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) action(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<Task> action)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) await action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<T, Task> action)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) await action(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<Task<T>> fn)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<T, Task<T>> fn)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) return await fn(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<Task<Failure>> fn)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<T, Task<Failure>> fn)
        {
            var outcome = await asyncPromise;

            if (outcome.IsSuccessful && condition) return await fn(outcome.ResultOrThrow());

            return outcome;
        }
    }
}
