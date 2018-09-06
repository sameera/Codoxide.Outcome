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
            ) => @this.Then(@this.IsSuccessful && predicate(@this.Result), action);

        public static Outcome<T> Then<T>(
                this Outcome<T> @this, 
                Predicate<T> predicate, 
                Action<T> action
            ) => @this.Then(@this.IsSuccessful && predicate(@this.Result), action);

        public static Outcome<T> Then<T>(
                this Outcome<T> @this,
                Predicate<T> predicate,
                Func<T> fn
            ) => @this.Then(@this.IsSuccessful && predicate(@this.Result), fn);

        public static Outcome<T> Then<T>(
                this Outcome<T> @this,
                Predicate<T> predicate,
                Func<Failure> fn
            ) => @this.Then(@this.IsSuccessful && predicate(@this.Result), fn);

        //public static Outcome<T> When<T, OutType>(
        //        this Outcome<T> @this, 
        //        Predicate<T> predicate, 
        //        out OutType output, 
        //        OutAction<OutType> action
        //    ) => @this.When(@this.IsSuccessful && predicate(@this.Result), out output, action);

        //public static Outcome<T> When<T, OutType>(
        //        this Outcome<T> @this, 
        //        Predicate<T> predicate, 
        //        out OutType output, 
        //        OutAction<T, OutType> action
        //    ) => @this.When(@this.IsSuccessful && predicate(@this.Result), out output, action);

        //public static Outcome<T> When<T, OutType>(
        //        this Outcome<T> @this, 
        //        Predicate<T> predicate, 
        //        out OutType output, OutFunc<OutType, Outcome<T>> fn) => @this.When(@this.IsSuccessful && predicate(@this.Result), out output, fn);

        //public static Outcome<T> When<T, OutType>(
        //        this Outcome<T> @this, 
        //        Predicate<T> predicate, 
        //        out OutType output, 
        //        ParameterziedOutFunc<T, OutType, Outcome<T>> fn) => @this.When(@this.IsSuccessful && predicate(@this.Result), out output, fn);

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Action action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Action<T> action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) action(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Task> action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) await action();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Task> action)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) await action(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, T> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return fn(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Failure> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Failure> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return fn(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Task<T>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Task<T>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return await fn(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<Task<Failure>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return await fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, Predicate<T> condition, Func<T, Task<Failure>> fn)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result)) return await fn(outcome.Result);

            return outcome;
        }
    }
}
