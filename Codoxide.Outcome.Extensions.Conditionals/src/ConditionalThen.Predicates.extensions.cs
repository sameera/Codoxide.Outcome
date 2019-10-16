using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeConditionalThenPredicatesExtensions
    {
        public static Outcome<T> Then<T>(
                this Outcome<T> @this, 
                Predicate<T> predicate, 
                Action action
            ) => @this.Then(@this.IsSuccessful && predicate(@this.ResultOrThrow()), action);

        public static Outcome<T> Then<T>(
                this Outcome<T> @this, 
                Predicate<T> predicate, 
                Action<T> action
            ) => @this.Then(@this.IsSuccessful && predicate(@this.ResultOrThrow()), action);

        public static Outcome<T> Then<T>(
                this Outcome<T> @this,
                Predicate<T> predicate,
                Func<T> fn
            ) => @this.Then(@this.IsSuccessful && predicate(@this.ResultOrThrow()), fn);

        public static Outcome<T> Then<T>(
                this Outcome<T> @this,
                Predicate<T> predicate,
                Func<Failure> fn
            ) => @this.Then(@this.IsSuccessful && predicate(@this.ResultOrThrow()), fn);

        public static Task<Outcome<T>> Then<T>(
                this Outcome<T> @this,
                Predicate<T> predicate,
                Func<Task> action) => @this.Then(@this.IsSuccessful && predicate(@this.ResultOrThrow()), action);

        public static Task<Outcome<T>> Then<T>(
                this Outcome<T> @this,
                Predicate<T> predicate,
                Func<T, Task> action
            ) => @this.Then(@this.IsSuccessful && predicate(@this.ResultOrThrow()), action);

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Action action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Action<T> action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) action(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Task> action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) await action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Task> action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) await action(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, T> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return fn(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Failure> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Failure> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return fn(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Task<T>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Task<T>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return await fn(outcome.ResultOrThrow());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Task<Failure>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Task<Failure>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrThrow())) return await fn(outcome.ResultOrThrow());

            return outcome;
        }
    }
}
