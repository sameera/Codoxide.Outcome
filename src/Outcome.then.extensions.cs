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

        public static Outcome<T> Then<T, U>(this Outcome<T> outcome, out U output, OutAction<U> action)
        {
            if (outcome.IsSuccessful)
            {
                action(out output);
            }
            else
            {
                output = default(U);
            }

            return outcome;
        }

        public static Outcome<T> Then<T, U>(this Outcome<T> outcome, out U output, OutAction<T, U> action)
        {
            if (outcome.IsSuccessful)
            {
                action(outcome.Result, out output);
            }
            else
            {
                output = default(U);
            }

            return outcome;
        }

        public static Outcome<T> Then<T>(this Outcome<T> outcome, Func<Outcome<T>> fn)
        {
            if (outcome.IsSuccessful) return fn();

            return outcome;
        }

        public static Outcome<U> Then<T, U>(this Outcome<T> outcome, Func<Outcome<U>> fn)
        {
            if (outcome.IsSuccessful) return fn();

            return Outcome<U>.Reject(outcome.Failure);
        }

        public static Outcome<U> Then<T, U>(this Outcome<T> outcome, Func<U> fn)
        {
            if (outcome.IsSuccessful) return new Outcome<U>(fn());

            return Outcome<U>.Reject(outcome.Failure);
        }

        public static Outcome<ReturnType> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, ReturnType> fn)
        {
            if (outcome.IsSuccessful) return new Outcome<ReturnType>(fn(outcome.Result));

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static Outcome<T> Then<T, U>(this Outcome<T> outcome, out U output, OutFunc<U, Outcome<T>> fn)
        {
            if (outcome.IsSuccessful)
            {
                return fn(out output);
            }
            else
            {
                output = default(U);
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
