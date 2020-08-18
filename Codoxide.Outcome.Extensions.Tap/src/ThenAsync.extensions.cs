using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;
    public static class OutcomeThenAsyncExtensions
    {
        [Obsolete("Use 'Tap' instead")]
		public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<Task> asyncAction)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }

        [Obsolete("Use 'Tap' instead")]
		public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<T, Task> asyncAction)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }
        
        [Obsolete("Use 'Tap' instead")]
		public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action action)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) action();

                return outcome;
            });
        }

        [Obsolete("Use 'Tap' instead")]
		public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action<T> action)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) action(outcome.ResultOrDefault());

                return outcome;
            });
        }

        [Obsolete("Use 'Tap' instead")]
		public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<T, Task> asyncAction)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }

        [Obsolete("Use 'Tap' instead")]
		public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task> asyncAction)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }
        
        private static async Task<Outcome<T>> Try<T>(Func<Task<Outcome<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return Outcome<T>.Reject(Fail(ex));
            }
        }
    }
}
