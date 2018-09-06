using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeWhenOtherwiseExtensions
    {
        public static Outcome<T> Then<T>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Action when,
            Action otherwise)
        {
            if (@this.IsSuccessful && condition(@this.Result))
            {
                when();
            }
            else if (@this.IsSuccessful)
            {
                otherwise();
            }
            return @this;
        }

        public static Outcome<T> Then<T>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Action<T> when,
            Action<T> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.Result))
            {
                when(@this.Result);
            }
            else if (@this.IsSuccessful)
            {
                otherwise(@this.Result);
            }
            return @this;
        }

        public static Outcome<R> Then<T, R>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Func<Outcome<R>> when,
            Func<Outcome<R>> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.Result))
            {
                return when();
            }
            else if (@this.IsSuccessful)
            {
                return otherwise();
            }
            return Outcome<R>.Reject(@this.Failure);
        }

        public static Outcome<R> Then<T, R>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Func<T, Outcome<R>> when,
            Func<T, Outcome<R>> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.Result))
            {
                return when(@this.Result);
            }
            else if (@this.IsSuccessful)
            {
                return otherwise(@this.Result);
            }
            return Outcome<R>.Reject(@this.Failure);
        }

        public static Outcome<R> Then<T, R>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Func<T, Outcome<R>> when,
            Func<T, Failure> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.Result))
            {
                return when(@this.Result);
            }
            else if (@this.IsSuccessful)
            {
                return otherwise(@this.Result);
            }
            return Outcome<R>.Reject(@this.Failure);
        }

        public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this, 
            Predicate<T> condition, 
            Func<Task<Outcome<R>>> when, 
            Func<Task<Outcome<R>>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result))
            {
                return await when();
            }
            else if (outcome.IsSuccessful)
            {
                return await otherwise();
            }
            return Outcome<R>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this, 
            Predicate<T> condition, 
            Func<T, Task<Outcome<R>>> when, 
            Func<T, Task<Outcome<R>>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result))
            {
                return await when(outcome.Result);
            }
            else if (outcome.IsSuccessful)
            {
                return await otherwise(outcome.Result);
            }
            return Outcome<R>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this,
            Predicate<T> condition,
            Func<T, Task<Outcome<R>>> when,
            Func<T, Failure> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.Result))
            {
                return await when(outcome.Result);
            }
            else if (outcome.IsSuccessful)
            {
                return otherwise(outcome.Result);
            }
            return Outcome<R>.Reject(outcome.Failure);
        }
    }
}
