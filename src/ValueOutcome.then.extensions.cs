using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codoxide
{
    public static partial class ValueOutcomeExtensions
    {
        public static (T result, Failure failure) Then<T>(this (T result, Failure failure) outcome, Action action)
        {
            if (outcome.failure == null) action();

            return outcome;
        }

        public static (T result, Failure failure) Then<T>(this (T result, Failure failure) outcome, Action<T> action)
        {
            if (outcome.failure == null) action(outcome.result);

            return outcome;
        }

        public static (T result, Failure failure) Then<T, OutType>(this (T result, Failure failure) outcome, out OutType output, OutAction<OutType> action)
        {
            if (outcome.failure == null)
            {
                action(out output);
            }
            else
            {
                output = default(OutType);
            }

            return outcome;
        }

        public static (T result, Failure failure) Then<T, OutType>(this (T result, Failure failure) outcome, out OutType output, OutAction<T, OutType> action)
        {
            if (outcome.failure == null)
            {
                action(outcome.result, out output);
            }
            else
            {
                output = default(OutType);
            }

            return outcome;
        }

        public static (T result, Failure failure) Then<T>(this (T result, Failure failure) outcome, Func<(T result, Failure failure)> fn)
        {
            if (outcome.failure == null) return fn();

            return outcome;
        }

        public static (T result, Failure failure) Then<T>(this (T result, Failure failure) outcome, Func<T, (T result, Failure failure)> fn)
        {
            if (outcome.failure == null) return fn(outcome.result);

            return outcome;
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this (T result, Failure failure) outcome, Func<(ResultType result, Failure failure)> fn)
        {
            if (outcome.failure == null) return fn();

            return (default(ResultType), outcome.failure);
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this (T result, Failure failure) outcome, Func<ResultType> fn)
        {
            if (outcome.failure == null) return (fn(), null);

            return (default(ResultType), outcome.failure);
        }

        public static (ResultType result, Failure failure) Then<T, ResultType>(this (T result, Failure failure) outcome, Func<T, ResultType> fn)
        {
            if (outcome.failure == null) return (fn(outcome.result), null);

            return (default(ResultType), outcome.failure);
        }

        public static (T result, Failure failure) Then<T, OutType>(this (T result, Failure failure) outcome, out OutType output, OutFunc<OutType, (T result, Failure failure)> fn)
        {
            if (outcome.failure == null)
            {
                return fn(out output);
            }
            else
            {
                output = default(OutType);
                return outcome;
            }
        }

        public static (T result, Failure failure) Then<T, OutType>(this (T result, Failure failure) outcome, out OutType output, ParameterziedOutFunc<T, OutType, (T result, Failure failure)> fn)
        {
            if (outcome.failure == null)
            {
                return fn(outcome.result, out output);
            }
            else
            {
                output = default(OutType);
                return outcome;
            }
        }

        public static (ResultType result, Failure failure) Then<T, OutType, ResultType>(this (T result, Failure failure) outcome, out OutType output, ParameterziedOutFunc<T, OutType, ResultType> fn)
        {
            if (outcome.failure == null)
            {
                return (fn(outcome.result, out output), null);
            }
            else
            {
                output = default(OutType);
                return (default(ResultType), outcome.failure);
            }
        }

        public static (T result, Failure failure) ThenIf<T>(this (T result, Failure failure) outcome, bool condition, Action action)
        {
            if (outcome.result != null && condition) action();

            return outcome;
        }

        public static (T result, Failure failure) ThenIf<T>(this (T result, Failure failure) outcome, bool condition, Func<(T result, Failure failure)> fn)
        {
            if (outcome.result != null && condition) return fn();

            return outcome;
        }
    }
}
