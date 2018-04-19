using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    static partial class ValueOutcomeExtensions
    {
        public static async Task<(T result, Failure failure)> Then<T>(this (T result, Failure failure) outcome, Func<Task> asyncAction)
        {
            if (outcome.failure == null) await asyncAction();

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this (T result, Failure failure) outcome, Func<Task<T>> asyncFunc)
        {
            if (outcome.failure == null) return (await asyncFunc(), null);

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this (T result, Failure failure) outcome, Func<Task<(T result, Failure failure)>> asyncFunc)
        {
            if (outcome.failure == null) return await asyncFunc();

            return outcome;
        }

        public static async Task<(T result, Failure failure)> Then<T>(this (T result, Failure failure) outcome, Func<T, Task<(T result, Failure failure)>> asyncFunc)
        {
            if (outcome.failure == null) return await asyncFunc(outcome.result);

            return outcome;
        }

        public static async Task<(ReturnType result, Failure failure)> Then<T, ReturnType>(this (T result, Failure failure) outcome, Func<Task<ReturnType>> asyncFunc)
        {
            if (outcome.failure == null) return (await asyncFunc(), null);

            return (default(ReturnType), outcome.failure);
        }

        public static async Task<(ReturnType result, Failure failure)> Then<T, ReturnType>(this (T result, Failure failure) outcome, Func<T, Task<ReturnType>> asyncFunc)
        {
            if (outcome.failure == null) return (await asyncFunc(outcome.result), null);

            return (default(ReturnType), outcome.failure);
        }

        public static async Task<(ReturnType result, Failure failure)> Then<T, ReturnType>(this (T result, Failure failure) outcome, Func<Task<(ReturnType result, Failure failure)>> asyncFunc)
        {
            if (outcome.failure == null) return await asyncFunc();

            return (default(ReturnType), outcome.failure);
        }

        public static async Task<(ReturnType result, Failure failure)> Then<T, ReturnType>(this (T result, Failure failure) outcome, Func<T, Task<(ReturnType result, Failure failure)>> asyncFunc)
        {
            if (outcome.failure == null) return await asyncFunc(outcome.result);

            return (default(ReturnType), outcome.failure);
        }
    }
}
