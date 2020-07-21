using Codoxide.Outcomes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class Outcome
    {
        public static int IntendedFailureCode => -1;

        public static Task<Outcome<T>> Of<T>(Func<Task<T>> func) => Of(func());

        public static Task<Outcome<T>> Of<T>(Task<T> task) =>
            task.ContinueWith(t => {
                if (t.IsFaulted && t.Exception?.InnerExceptions?.Count == 1)
                {
                    return Outcome<T>.Reject(t.Exception.InnerExceptions.First());
                }
                else if (t.IsFaulted && t.Exception != null)
                {
                    return Outcome<T>.Reject(t.Exception);
                }
                else if (t.IsFaulted)
                {
                    return Outcome<T>.Reject("Task failed unexpectedly.");
                }
                else if (t.IsCanceled)
                {
                    return Outcome<T>.Reject("Task was cancelled", new TaskCanceledException(t));
                }
                else
                {
                    return new Outcome<T>(t.Result);
                }
            });

        public static Outcome<T> Of<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return new Outcome<T>(result);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static Outcome<T> Of<T>(T result) => new Outcome<T>(result);


        /// <summary>
        /// Returns an outcome, that returns "nothing". Useful in situations where you have conditionals that
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
        public static Outcome<Nop> Any() => new Outcome<Nop>(Nop.Void);

        public static Outcome<Nop> Any(Action action)
        {
            try
            {
                action();
                return Any();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                return Reject(e);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

    }
}
