using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeAsyncExtensions
    {
        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<Task> asyncAction)
        {
            if (outcome.IsSuccessful) await asyncAction();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<T, Task> asyncAction)
        {
            if (outcome.IsSuccessful) await asyncAction(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<ReturnType>> asyncFunc)
        {
            if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<ReturnType>> asyncFunc)
        {
            if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.Result));

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<Outcome<ReturnType>>> asyncFunc)
        {
            if (outcome.IsSuccessful) return await asyncFunc();

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<T, Task<Outcome<ReturnType>>> asyncFunc)
        {
            if (outcome.IsSuccessful) return await asyncFunc(outcome.Result);

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }
    }
}
