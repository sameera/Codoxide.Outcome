namespace Codoxide
{
    public static class OutcomeAsyncExtensions
    {
        [Obsolete("Use the Tap method from Codoxide.Outcome.Extensions.Tap instead.")]
        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<Task> asyncAction)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }

        [Obsolete("Use the Tap method from Codoxide.Outcome.Extensions.Tap instead.")]
        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<T, Task> asyncAction)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<Outcome<ReturnType>>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return await asyncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<Outcome<ReturnType>>> asyncFunc)
        {
            return await Try(async () => {
                if (outcome.IsSuccessful) return await asyncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
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
