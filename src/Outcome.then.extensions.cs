using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    static partial class OutcomeExtensions
    {
        public static Outcome<T> Then<T>(this Outcome<T> outcome, Action action)
        {
            if (outcome.IsSuccessful) action();

            return outcome;
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, Action<T> action)
        {
            if (outcome.IsSuccessful) action(outcome.Result);

            return outcome;
        }

        public static Outcome<T> Then<T, OutType>(this Outcome<T> outcome, out OutType output, OutAction<OutType> action)
        {
            if (outcome.IsSuccessful)
            {
                action(out output);
            }
            else
            {
                output = default(OutType);
            }

            return outcome;
        }

        public static Outcome<T> Then<T, OutType>(this Outcome<T> outcome, out OutType output, OutAction<T, OutType> action)
        {
            if (outcome.IsSuccessful)
            {
                action(outcome.Result, out output);
            }
            else
            {
                output = default(OutType);
            }

            return outcome;
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, Func<Outcome<T>> fn)
        {
            if (outcome.IsSuccessful) return fn();

            return outcome;
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, Func<T, Outcome<T>> fn)
        {
            if (outcome.IsSuccessful) return fn(outcome.Result);

            return outcome;
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> outcome, Func<ResultType> fn)
        {
            if (outcome.IsSuccessful) return new Outcome<ResultType>(fn());

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> outcome, Func<Outcome<ResultType>> fn)
        {
            if (outcome.IsSuccessful) return fn();

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> outcome, Func<ValueTuple<ResultType, Failure>> fn)
        {
            if (outcome.IsSuccessful) return (Outcome<ResultType>)fn();

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> outcome, Func<T, ResultType> fn)
        {
            if (outcome.IsSuccessful) return new Outcome<ResultType>(fn(outcome.Result));

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static Outcome<ResultType> Then<T, ResultType>(this Outcome<T> outcome, Func<T, ValueTuple<ResultType, Failure>> fn)
        {
            if (outcome.IsSuccessful) return (Outcome<ResultType>)fn(outcome.Result);

            return Outcome<ResultType>.Reject(outcome.Failure);
        }

        public static Outcome<T> Then<T, OutType>(this Outcome<T> outcome, out OutType output, OutFunc<OutType, Outcome<T>> fn)
        {
            if (outcome.IsSuccessful)
            {
                return fn(out output);
            }
            else
            {
                output = default(OutType);
                return outcome;
            }
        }

        public static Outcome<T> Then<T, OutType>(this Outcome<T> outcome, out OutType output, ParameterziedOutFunc<T, OutType, Outcome<T>> fn)
        {
            if (outcome.IsSuccessful)
            {
                return fn(outcome.Result, out output);
            }
            else
            {
                output = default(OutType);
                return Outcome<T>.Reject(outcome.Failure);
            }
        }

        public static Outcome<ResultType> Then<T, OutType, ResultType>(this Outcome<T> outcome, out OutType output, ParameterziedOutFunc<T, OutType, ResultType> fn)
        {
            if (outcome.IsSuccessful)
            {
                return new Outcome<ResultType>(fn(outcome.Result, out output));
            }
            else
            {
                output = default(OutType);
                return Outcome<ResultType>.Reject(outcome.Failure);
            }
        }

        public static Outcome<T> ThenIf<T>(this Outcome<T> outcome, bool condition, Action action)
        {
            if (outcome.IsSuccessful && condition) action();

            return outcome;
        }

        public static Outcome<T> ThenIf<T>(this Outcome<T> outcome, bool condition, Func<Outcome<T>> fn)
        {
            if (outcome.IsSuccessful && condition) return fn();

            return outcome;
        }
    }
}
