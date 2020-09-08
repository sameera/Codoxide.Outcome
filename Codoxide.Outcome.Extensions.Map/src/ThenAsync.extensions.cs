using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;
    using static Codoxide.Internals.Utility;

    public static class OutcomeThenAsyncExtensions
    {
        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<Outcome<ReturnType>>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return await asyncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        [Obsolete("Use 'Map' instead")]
		public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<Outcome<ReturnType>>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return await asyncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }
    }
}
