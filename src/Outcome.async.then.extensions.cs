using System;
using System.Threading.Tasks;

namespace Codoxide
{
    static partial class OutcomeExtensions
    {
        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<Task> asyncAction)
        {
            if (outcome.IsSuccessful) await asyncAction();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<Task<T>> asyncFunc)
        {
            if (outcome.IsSuccessful) return new Outcome<T>(await asyncFunc());

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<Task<Outcome<T>>> asyncFunc)
        {
            if (outcome.IsSuccessful) return await asyncFunc();

            return outcome;
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<T, Task<Outcome<T>>> asyncFunc)
        {
            if (outcome.IsSuccessful) return await asyncFunc(outcome.Result);

            return outcome;
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Outcome<T> outcome, Func<Task<ReturnType>> asyncFunc)
        {
            if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

            return Outcome<ReturnType>.Reject(outcome.Failure);
        }

        //public static async Task<Outcome<T>> Then<T>(this Outcome<T> outcome, Func<T, Task<T>> asyncFunc)
        //{
        //    if (outcome.IsSuccessful) return new Outcome<T>(await asyncFunc(outcome.Result));

        //    return outcome;
        //}

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
