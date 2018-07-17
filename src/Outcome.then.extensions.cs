using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    static partial class OutcomeExtensions
    {
        public static Outcome<T> Then<T>(this Outcome<T> @this, Action action) => @this.When(true, action);

        public static Outcome<T> Then<T>(this Outcome<T> @this, Action<T> action) => @this.When(true, action);

        public static Outcome<T> Then<T, OutType>(this Outcome<T> @this, out OutType output, OutAction<OutType> action) => @this.When(true, out output, action);

        public static Outcome<T> Then<T, OutType>(this Outcome<T> @this, out OutType output, OutAction<T, OutType> action) => @this.When(true, out output, action);

        public static Outcome<T> Then<T, OutType>(this Outcome<T> @this, out OutType output, OutFunc<OutType, Outcome<T>> fn) => @this.When(true, out output, fn);

        public static Outcome<T> Then<T, OutType>(this Outcome<T> @this, out OutType output, ParameterziedOutFunc<T, OutType, Outcome<T>> fn) => @this.When(true, out output, fn);

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<ResultType> fn)
        {
            if (@this.IsSuccessful) return new Outcome<ResultType>(fn());

            return Outcome<ResultType>.Reject(@this.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn)
        {
            if (@this.IsSuccessful) return fn();

            return Outcome<ResultType>.Reject(@this.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, Outcome<ResultType>> fn)
        {
            if (@this.IsSuccessful) return fn(@this.Result);

            return Outcome<ResultType>.Reject(@this.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<ValueTuple<ResultType, Failure>> fn)
        {
            if (@this.IsSuccessful) return (Outcome<ResultType>)fn();

            return Outcome<ResultType>.Reject(@this.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, ResultType> fn)
        {
            if (@this.IsSuccessful) return new Outcome<ResultType>(fn(@this.Result));

            return Outcome<ResultType>.Reject(@this.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> @this, Func<T, ValueTuple<ResultType, Failure>> fn)
        {
            if (@this.IsSuccessful) return (Outcome<ResultType>)fn(@this.Result);

            return Outcome<ResultType>.Reject(@this.Failure);
        }

        public static Outcome<ResultType> When<T, OutType, ResultType>(this Outcome<T> @this, out OutType output, ParameterziedOutFunc<T, OutType, ResultType> fn)
        {
            if (@this.IsSuccessful)
            {
                return fn(@this.Result, out output);
            }
            else
            {
                output = default(OutType);
                return Outcome<ResultType>.Reject(@this.Failure);
            }
        }
    }
}
