using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using Codoxide.Internals;

    public static partial class TapWhenExtensions
    {
        public static async Task<Outcome<T>> TapWhen<T>(this Outcome<T> outcome, bool condition, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (condition && outcome.IsSuccessful) await asyncAction().ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Outcome<T> outcome, bool condition, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (condition && outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault()).ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (condition && outcome.IsSuccessful) action();

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action<T> action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (condition && outcome.IsSuccessful) action(outcome.ResultOrDefault());

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (condition && outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault()).ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }

        public static async Task<Outcome<T>> TapWhen<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise.ConfigureAwait(false);
                if (condition && outcome.IsSuccessful) await asyncAction().ConfigureAwait(false);

                return outcome;
            }).ConfigureAwait(false);
        }
    }
}