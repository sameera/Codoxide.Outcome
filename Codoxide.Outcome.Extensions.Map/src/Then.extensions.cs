using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    public static class OutcomeThenThenExtensions
    {

        [Obsolete("Use 'Map' instead")]
        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Outcome.Of(fn);
        }

        [Obsolete("Use 'Map' instead")]
        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try(fn);
        }

        [Obsolete("Use 'Map' instead")]
        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Outcome.Of(() => fn(@this.ResultOrDefault()));
        }

        [Obsolete("Use 'Map' instead")]
        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, Outcome<ResultType>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try(() => fn(@this.ResultOrDefault()));
        }

        [Obsolete("Use 'Map' instead")]
        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<ValueTuple<ResultType, Failure>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try<ResultType>(() => fn());
        }

        [Obsolete("Use 'Map' instead")]
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
