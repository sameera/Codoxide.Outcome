namespace Codoxide
{
    public static class OutcomeWhenOtherwiseExtensions
    {
        [Obsolete]
		public static Outcome<T> Then<T>(
            this Outcome<T> @this,
            bool condition,
            Action when,
            Action otherwise)
        {
            if (@this.IsSuccessful && condition)
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
            bool condition,
            Action<T> when,
            Action<T> otherwise)
        {
            if (@this.IsSuccessful && condition)
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
            bool condition,
            Func<Outcome<R>> when,
            Func<Outcome<R>> otherwise)
        {
            if (@this.IsSuccessful && condition)
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
            bool condition,
            Func<T, Outcome<R>> when,
            Func<T, Outcome<R>> otherwise)
        {
            if (@this.IsSuccessful && condition)
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
            bool condition,
            Func<T, Outcome<R>> when,
            Func<T, Failure> otherwise)
        {
            if (@this.IsSuccessful && condition)
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
            bool condition,
            Func<Task<Outcome<R>>> when,
            Func<Task<Outcome<R>>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition)
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
            bool condition,
            Func<Outcome<R>> when,
            Func<Outcome<R>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition)
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
            bool condition,
            Func<T, Outcome<R>> when,
            Func<T, Outcome<R>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition)
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
            bool condition,
            Func<T, R> when,
            Func<T, R> otherwise) where R : class
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition)
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
            bool condition,
            Func<T, Task<Outcome<R>>> when,
            Func<T, Task<Outcome<R>>> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition)
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
            bool condition,
            Func<T, Task<Outcome<R>>> when,
            Func<T, Failure> otherwise)
        {
            var outcome = await @this;

            if (outcome.IsSuccessful && condition)
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
