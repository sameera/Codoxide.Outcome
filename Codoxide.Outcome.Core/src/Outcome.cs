using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;

    public static class Outcome
    {
        public static int IntendedFailureCode => -1;

        public static Task<Outcome<T>> Of<T>(Func<Task<T>> func)
        {
            return Of(func());
        }

        public static Task<Outcome<T>> Of<T>(Task<T> task)
        {
            return task.ContinueWith(t => {
                    if (t.IsCompleted)
                    {
                        return new Outcome<T>(t.Result);
                    }
                    else if (t.IsFaulted && t.Exception != null)
                    {
                        return Outcome<T>.Reject(t.Exception.Message, t.Exception);
                    }
                    else if (t.IsCanceled)
                    {
                        return Outcome<T>.Reject("Task was cancelled", new TaskCanceledException(t));
                    }
                    else
                    {
                        return Outcome<T>.Reject("Task failed unexpectedly.");
                    }
                });
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
        public static Outcome<Void> Any() => new Outcome<Void>();

        public static Outcome<Void> Never() => new Outcome<Void>(new Outcomes.Failure("Intended Failure", IntendedFailureCode));

        public static Outcome<Void> Reject(string reason) => new Outcome<Void>(new Failure(reason));

        public static Outcome<Void> Reject(string reason, Exception exception) => new Outcome<Void>(new Failure(reason, exception));

        internal static Outcome<Void> Reject(Failure failure) => new Outcome<Void>(failure);
    }
}
