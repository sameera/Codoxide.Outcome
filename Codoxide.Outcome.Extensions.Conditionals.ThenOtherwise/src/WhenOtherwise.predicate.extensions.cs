using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeWhenOtherwisePredicateExtensions
    {
        [Obsolete]
		public static Outcome<T> Then<T>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Action when,
            Action otherwise)
        {
            if (@this.IsSuccessful && condition(@this.ResultOrDefault()))
            {
                when();
            }
            else if (@this.IsSuccessful)
            {
                otherwise();
            }
            return @this;
        }

        [Obsolete]
		public static Outcome<T> Then<T>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Action<T> when,
            Action<T> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.ResultOrDefault()))
            {
                when(@this.ResultOrDefault());
            }
            else if (@this.IsSuccessful)
            {
                otherwise(@this.ResultOrDefault());
            }
            return @this;
        }

        [Obsolete]
		public static Outcome<R> Then<T, R>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Func<Outcome<R>> when,
            Func<Outcome<R>> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.ResultOrDefault()))
            {
                return when();
            }
            else if (@this.IsSuccessful)
            {
                return otherwise();
            }
            return Outcome<R>.Reject(@this.FailureOrThrow());
        }

        [Obsolete]
		public static Outcome<R> Then<T, R>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Func<T, Outcome<R>> when,
            Func<T, Outcome<R>> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.ResultOrDefault()))
            {
                return when(@this.ResultOrDefault());
            }
            else if (@this.IsSuccessful)
            {
                return otherwise(@this.ResultOrDefault());
            }
            return Outcome<R>.Reject(@this.FailureOrThrow());
        }

        [Obsolete]
		public static Outcome<R> Then<T, R>(
            this Outcome<T> @this,
            Predicate<T> condition,
            Func<T, Outcome<R>> when,
            Func<T, Failure> otherwise)
        {
            if (@this.IsSuccessful && condition(@this.ResultOrDefault()))
            {
                return when(@this.ResultOrDefault());
            }
            else if (@this.IsSuccessful)
            {
                return otherwise(@this.ResultOrDefault());
            }
            return Outcome<R>.Reject(@this.FailureOrThrow());
        }

        [Obsolete]
		public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this, 
            Predicate<T> condition, 
            Func<Task<Outcome<R>>> when, 
            Func<Task<Outcome<R>>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrDefault()))
            {
                return await when();
            }
            else if (outcome.IsSuccessful)
            {
                return await otherwise();
            }
            return Outcome<R>.Reject(outcome.FailureOrThrow());
        }

        [Obsolete]
		public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this, 
            Predicate<T> condition, 
            Func<Outcome<R>> when, 
            Func<Outcome<R>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrDefault()))
            {
                return when();
            }
            else if (outcome.IsSuccessful)
            {
                return otherwise();
            }
            return Outcome<R>.Reject(outcome.FailureOrThrow());
        }

        [Obsolete]
		public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this, 
            Predicate<T> condition, 
            Func<T, Outcome<R>> when, 
            Func<T, Outcome<R>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrDefault()))
            {
                return when(outcome.ResultOrDefault());
            }
            else if (outcome.IsSuccessful)
            {
                return otherwise(outcome.ResultOrDefault());
            }
            return Outcome<R>.Reject(outcome.FailureOrThrow());
        }

        [Obsolete]
		public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this,
            Predicate<T> condition,
            Func<T, R> when,
            Func<T, R> otherwise) where R: class
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrDefault()))
            {
                return when(outcome.ResultOrDefault());
            }
            else if (outcome.IsSuccessful)
            {
                return otherwise(outcome.ResultOrDefault());
            }
            return Outcome<R>.Reject(outcome.FailureOrThrow());
        }

        [Obsolete]
		public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this, 
            Predicate<T> condition, 
            Func<T, Task<Outcome<R>>> when, 
            Func<T, Task<Outcome<R>>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrDefault()))
            {
                return await when(outcome.ResultOrDefault());
            }
            else if (outcome.IsSuccessful)
            {
                return await otherwise(outcome.ResultOrDefault());
            }
            return Outcome<R>.Reject(outcome.FailureOrThrow());
        }

        [Obsolete]
		public static async Task<Outcome<R>> Then<T, R>(
            this Task<Outcome<T>> @this,
            Predicate<T> condition,
            Func<T, Task<Outcome<R>>> when,
            Func<T, Failure> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition(outcome.ResultOrDefault()))
            {
                return await when(outcome.ResultOrDefault());
            }
            else if (outcome.IsSuccessful)
            {
                return otherwise(outcome.ResultOrDefault());
            }
            return Outcome<R>.Reject(outcome.FailureOrThrow());
        }
    }
}
