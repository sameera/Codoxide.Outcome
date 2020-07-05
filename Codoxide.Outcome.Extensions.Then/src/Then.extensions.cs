using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    public static class OutcomeThenExtensions
    {
        [Obsolete("Use the Tap method from Codoxide.Outcome.Extensions.Tap instead.")]
        public static Outcome<T> Then<T>(this Outcome<T> @this, Action action)
        {
            if (!@this.IsSuccessful) return @this;

            return Try(() => {
                action();
                return @this;
            });
        }

        [Obsolete("Use the Tap method from Codoxide.Outcome.Extensions.Tap instead.")]
        public static Outcome<T> Then<T>(this Outcome<T> @this, Action<T> action)
        {
            if (!@this.IsSuccessful) return @this;

            return Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Outcome.Of(fn);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try(fn);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Outcome.Of(() => fn(@this.ResultOrDefault()));
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, Outcome<ResultType>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try(() => fn(@this.ResultOrDefault()));
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<ValueTuple<ResultType, Failure>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try<ResultType>(() => fn());
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, ValueTuple<ResultType, Failure>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try<ResultType>(() => fn(@this.ResultOrDefault()));
        }

        internal static Outcome<T> Try<T>(Func<Outcome<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
