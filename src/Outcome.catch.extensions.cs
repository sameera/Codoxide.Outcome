using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeCatchExtensions
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

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> asyncOutcome, Action action)
        {
            var outcome = await asyncOutcome;
            if (!outcome.IsSuccessful) action();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> asyncOutcome, Func<Outcome<T>> fn)
        {
            var outcome = await asyncOutcome;
            if (!outcome.IsSuccessful) return fn();

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> asyncOutcome, Action<Failure> action)
        {
            var outcome = await asyncOutcome;
            if (!outcome.IsSuccessful) action(outcome.Failure);

            return outcome;
        }

        public static async Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> asyncOutcome, Func<Failure, Outcome<T>> fn)
        {
            var outcome = await asyncOutcome;
            if (!outcome.IsSuccessful) return fn(outcome.Failure);

            return outcome;
        }
    }
}
