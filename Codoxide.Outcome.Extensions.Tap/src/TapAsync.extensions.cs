using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using Codoxide.Internals;

    public static class OutcomeTapAsyncExtensions
    {
        public static async Task<Outcome<T>> Tap<T>(this Outcome<T> outcome, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Outcome<T> outcome, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }
        
        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, Action action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) action();

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, Action<T> action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) action(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }
    }
}
