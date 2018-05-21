using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    public static partial class ValueOutcomeExtensions
    {
        public static (T result, Failure failure) Then<T>(this ValueTuple<T, Failure> outcome, Action action)
        {
            if (outcome.Item2 == null) action();

            return outcome;
        }

        public static (T result, Failure failure) Then<T>(this ValueTuple<T, Failure> outcome, Action<T> action)
        {
            if (outcome.Item2 == null) action(outcome.Item1);

            return outcome;
        }

        public static (T result, Failure failure) Then<T, OutType>(this ValueTuple<T, Failure> outcome, out OutType output, OutAction<OutType> action)
        {
            if (outcome.Item2 == null)
            {
                action(out output);
            }
            else
            {
                output = default(OutType);
            }

            return outcome;
        }

        public static (T result, Failure failure) Then<T, OutType>(this ValueTuple<T, Failure> outcome, out OutType output, OutAction<T, OutType> action)
        {
            if (outcome.Item2 == null)
            {
                action(outcome.Item1, out output);
            }
            else
            {
                output = default(OutType);
            }

            return outcome;
        }

        public static (T result, Failure failure) Then<T>(this ValueTuple<T, Failure> outcome, Func<ValueTuple<T, Failure>> fn)
        {
            if (outcome.Item2 == null) return fn();

            return outcome;
        }

        public static (T result, Failure failure) Then<T>(this ValueTuple<T, Failure> outcome, Func<T, ValueTuple<T, Failure>> fn)
        {
            if (outcome.Item2 == null) return fn(outcome.Item1);

            return outcome;
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this ValueTuple<T, Failure> outcome, Func<ValueTuple<ResultType, Failure>> fn)
        {
            if (outcome.Item2 == null) return fn();

            return (default(ResultType), outcome.Item2);
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this ValueTuple<T, Failure> outcome, Func<ResultType> fn)
        {
            if (outcome.Item2 == null) return (fn(), null);

            return (default(ResultType), outcome.Item2);
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this ValueTuple<T, Failure> outcome, Func<Outcome<ResultType>> fn)
        {
            if (outcome.Item2 == null) return fn();

            return (default(ResultType), outcome.Item2);
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this ValueTuple<T, Failure> outcome, Func<T, ResultType> fn)
        {
            if (outcome.Item2 == null) return (fn(outcome.Item1), null);

            return (default(ResultType), outcome.Item2);
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this ValueTuple<T, Failure> outcome, Func<T, Outcome<ResultType>> fn)
        {
            if (outcome.Item2 == null) return fn(outcome.Item1);

            return (default(ResultType), outcome.Item2);
        }

        public static (T result, Failure failure) Then<T, OutType>(this ValueTuple<T, Failure> outcome, out OutType output, OutFunc<OutType, ValueTuple<T, Failure>> fn)
        {
            if (outcome.Item2 == null)
            {
                return fn(out output);
            }
            else
            {
                output = default(OutType);
                return outcome;
            }
        }

        public static (T result, Failure failure) Then<T, OutType>(this ValueTuple<T, Failure> outcome, out OutType output, ParameterziedOutFunc<T, OutType, ValueTuple<T, Failure>> fn)
        {
            if (outcome.Item2 == null)
            {
                return fn(outcome.Item1, out output);
            }
            else
            {
                output = default(OutType);
                return outcome;
            }
        }

        public static (ResultType result, Failure failure) Then<T, OutType, ResultType>(this ValueTuple<T, Failure> outcome, out OutType output, ParameterziedOutFunc<T, OutType, ResultType> fn)
        {
            if (outcome.Item2 == null)
            {
                return (fn(outcome.Item1, out output), null);
            }
            else
            {
                output = default(OutType);
                return (default(ResultType), outcome.Item2);
            }
        }

        public static (T result, Failure failure) ThenIf<T>(this ValueTuple<T, Failure> outcome, bool condition, Action action)
        {
            if (outcome.Item2 == null && condition) action();

            return outcome;
        }

        public static (T result, Failure failure) ThenIf<T>(this ValueTuple<T, Failure> outcome, bool condition, Func<ValueTuple<T, Failure>> fn)
        {
            if (outcome.Item2 == null && condition) return fn();

            return outcome;
        }
    }
}
