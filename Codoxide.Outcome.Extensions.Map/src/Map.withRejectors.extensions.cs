using Codoxide.Outcomes;
using System;
using System.Diagnostics;

namespace Codoxide
{
    using static Codoxide.OutcomeInternals.Utility;
    
    public delegate Outcome<T> SimpleRejector<T>(string reason);
    public delegate Outcome<T> ExceptionRejector<T>(Exception exception);

    public static class OutcomeMapWithRejectorsExtensions
    {

        public static Outcome<ResultType> Map<T, ResultType>(this Outcome<T> @this, Func<ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            Outcome<T> rejector(string reason) {
                return  new Failure(reason);
            }
            
            return Outcome.Of(() => fn(rejector));
        }

        public static Outcome<ResultType> Map<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try(fn);
        }

        public static Outcome<ResultType> Map<T, ResultType>(this Outcome<T> @this, Func<T, ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Outcome.Of(() => fn(@this.ResultOrDefault()));
        }

        public static Outcome<ResultType> Map<T, ResultType>(this Outcome<T> @this, Func<T, Outcome<ResultType>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try(() => fn(@this.ResultOrDefault()));
        }

        public static Outcome<ResultType> Map<T, ResultType>(this Outcome<T> @this, Func<ValueTuple<ResultType, Failure>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try<ResultType>(() => fn());
        }

        public static Outcome<ResultType> Map<T, ResultType>(this Outcome<T> @this, Func<T, ValueTuple<ResultType, Failure>> fn)
        {
            if (!@this.IsSuccessful) return Outcome<ResultType>.Reject(@this.FailureOrNull());

            return Try<ResultType>(() => fn(@this.ResultOrDefault()));
        }
    }
}
