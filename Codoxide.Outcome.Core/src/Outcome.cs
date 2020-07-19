using Codoxide.Outcomes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class Outcome
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
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
        }

        public static Outcome<T> Of<T>(T result) => new Outcome<T>(result);


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
        public static Outcome<Nop> Any() => new Outcome<Nop>();

        public static Outcome<Nop> Any(Action action)
        {
            try
            {
                action();
                return Any();
            }
            catch (Exception e)
            {
                return Reject(e);
            }
        }

        public static Outcome<Nop> Never() => new Outcome<Nop>(new Outcomes.Failure("Intended Failure", IntendedFailureCode));

        public static Outcome<Nop> Reject(string reason) => new Outcome<Nop>(new Failure(reason));

        public static Outcome<Nop> Reject(string reason, Exception exception) => new Outcome<Nop>(new Failure(reason, exception));
        
        public static Outcome<Nop> Reject(Exception exception) => Reject(exception.Message, exception);

        internal static Outcome<Nop> Reject(Failure failure) => new Outcome<Nop>(failure);
    }
}
