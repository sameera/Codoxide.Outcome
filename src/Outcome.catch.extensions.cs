using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    static partial class OutcomeExtensions
    {
        public static Outcome<T> Catch<T>(this Outcome<T> outcome, Action action)
        {
            if (!outcome.IsSuccessful) action();

            return outcome;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> outcome, Func<Outcome<T>> fn)
        {
            if (!outcome.IsSuccessful) return fn();

            return outcome;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> outcome, Action<Failure> action)
        {
            if (!outcome.IsSuccessful) action(outcome.Failure);

            return outcome;
        }

        public static Outcome<T> Catch<T>(this Outcome<T> outcome, Func<Failure, Outcome<T>> fn)
        {
            if (!outcome.IsSuccessful) return fn(outcome.Failure);

            return outcome;
        }

    }
}
