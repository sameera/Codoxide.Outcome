using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class Outcome
    {
        public static async Task<Outcome<T>> Of<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();
                return new Outcome<T>(result);
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
        }

        public static async Task<Outcome<T>> Of<T>(Task<T> task)
        {
            try
            {
                var result = await task;
                return new Outcome<T>(result);
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
        }

        public static Outcome<T> Of<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return new Outcome<T>(result);
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
        }

        /// <summary>
        /// Returns an outcome, that returns true. Useful in situations where you have conditionals that
        /// need to be chained and don't have any other Outcome-returning initiation function.
        /// </summary>
        /// <example>
        ///     <code>
        ///     Outcome.Any()
        ///             .Then(isSomething, () => doSomething())
        ///             .Then(isOther, () => doOtherthing())
        ///     </code>
        /// </example>
        /// <returns></returns>
        public static Outcome<bool> Any() => true.AsOutcome();

        public static Outcome<T> AsOutcome<T>(this T @this) => new Outcome<T>(@this);


        public static Task<Outcome<T>> AsAsyncOutcome<T>(this T @this) => Task.FromResult(new Outcome<T>(@this));
    }
}
