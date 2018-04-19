using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    static partial class ValueOutcomeExtensions
    {
        public static (T result, Failure failure) Catch<T>(this (T result, Failure failure) outcome, Action action)
        {
            if (outcome.failure != null) action();

            return outcome;
        }

        public static (T result, Failure failure) Catch<T>(this (T result, Failure failure) outcome, Func<(T result, Failure failure)> fn)
        {
            if (outcome.failure != null) return fn();

            return outcome;
        }

        public static (T result, Failure failure) Catch<T>(this (T result, Failure failure) outcome, Action<Failure> action)
        {
            if (outcome.failure != null) action(outcome.failure);

            return outcome;
        }

        public static (T result, Failure failure) Catch<T>(this (T result, Failure failure) outcome, Func<Failure, (T result, Failure failure)> fn)
        {
            if (outcome.failure != null) return fn(outcome.failure);

            return outcome;
        }

    }
}
