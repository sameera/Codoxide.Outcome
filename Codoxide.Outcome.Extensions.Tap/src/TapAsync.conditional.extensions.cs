using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;
    public static class OutcomeTapAsyncConditionalExtensions
    {
        public static async Task<Outcome<T>> Tap<T>(this Outcome<T> outcome, bool condition, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (condition && outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Outcome<T> outcome, bool condition, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                if (condition && outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }
        
        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (condition && outcome.IsSuccessful) action();

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action<T> action)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (condition && outcome.IsSuccessful) action(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<T, Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (condition && outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Tap<T>(this Task<Outcome<T>> asyncPromise, bool condition, Func<Task> asyncAction)
        {
            return await Utility.Try(async () => {
                var outcome = await asyncPromise;
                if (condition && outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }
    }
}
