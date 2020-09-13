using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.OutcomeInternals.Utility;

    public static class OutcomeAsyncExtensions
    {
        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<Task<ReturnType>> asyncFunc)
        {
            if (outcome.IsSuccessful) return Outcome.Of(asyncFunc);

            return Outcome<ReturnType>
                .Reject(outcome.FailureOrNull())
                .ForAsync();
        }

        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<ReturnType>> asyncFunc)
        {
            if (outcome.IsSuccessful) return Outcome.Of(() => asyncFunc(outcome.ResultOrDefault()));

            return Outcome<ReturnType>.Reject(outcome
                    .FailureOrNull())
                    .ForAsync();
        }

        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<Task<Outcome<ReturnType>>> asyncFunc)
        {
            return Try(() => {
                if (outcome.IsSuccessful) return asyncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull()).ForAsync();
            });
        }

        public static Task<Outcome<ReturnType>> Map<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<Outcome<ReturnType>>> asyncFunc)
        {
            return Try(() => {
                if (outcome.IsSuccessful) return asyncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnType>
                        .Reject(outcome.FailureOrNull())
                        .ForAsync();
            });
        }
    }
}
