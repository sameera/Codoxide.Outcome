using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using Codoxide.OutcomeInternals;

    public static partial class TapWhenExtensions
    {
        /***************************
         * Parameterless Predicate
         **************************/

        public static async Task<Outcome<T>> TapWhen<T>(this Outcome<T> outcome, Func<bool> predicate, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (outcome.IsSuccessful && predicate()) await asyncAction().ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Outcome<T> outcome, Func<bool> predicate, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (outcome.IsSuccessful && predicate()) await asyncAction(outcome.ResultOrDefault()).ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<bool> predicate, Action action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate()) action();

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<bool> predicate, Action<T> action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate()) action(outcome.ResultOrDefault());

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<bool> predicate, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate()) await asyncAction(outcome.ResultOrDefault()).ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<bool> predicate, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate()) await asyncAction().ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        /***************************
         * Parametered Predicate
         **************************/

        public static async Task<Outcome<T>> TapWhen<T>(this Outcome<T> outcome, Func<T, bool> predicate, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (outcome.IsSuccessful && predicate(outcome.ResultOrThrow())) await asyncAction().ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Outcome<T> outcome, Func<T, bool> predicate, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (outcome.IsSuccessful && predicate(outcome.ResultOrThrow()))
                {
                    await asyncAction(outcome.ResultOrDefault()).ConfigureAwait(false);
                }
                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<T, bool> predicate, Action action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate(outcome.ResultOrThrow())) action();

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<T, bool> predicate, Action<T> action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate(outcome.ResultOrThrow())) action(outcome.ResultOrDefault());

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<T, bool> predicate, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate(outcome.ResultOrThrow()))
                {
                    await asyncAction(outcome.ResultOrDefault()).ConfigureAwait(false);
                }
                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, Func<T, bool> predicate, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (outcome.IsSuccessful && predicate(outcome.ResultOrThrow())) await asyncAction().ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }
    }
}