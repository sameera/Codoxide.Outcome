using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class Outcome
    {
        public static int IntendedFailureCode => -1;

        public static async Task<Outcome<T>> Of<T>(Func<Task<T>> func)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));

            try
            {
                var result = await func().ConfigureAwait(false);
                return new Outcome<T>(result);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Outcome<T>.Reject(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static async Task<Outcome<T>> Of<T>(Task<T> task)
        {
            _ = task ?? throw new ArgumentNullException(nameof(task));

            try
            {
                var result = await task.ConfigureAwait(false);
                return new Outcome<T>(result);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Outcome<T>.Reject(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static Outcome<T> Of<T>(Func<T> func)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));


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

        // Following is marked obsolete as it leads to improper use where 
        // results of an operation is passed instead of the delegate of the operation.
        [Obsolete("Use Outcome.FromResult instead")]
        public static Outcome<T> Of<T>(T result) => new Outcome<T>(result);

        public static Outcome<T> FromResult<T>(T result) => new Outcome<T>(result);

        public static async Task<Outcome<Nop>> Of(Task asyncAction)
        {
            _ = asyncAction ?? throw new ArgumentNullException(nameof(asyncAction));

            try
            {
                await asyncAction.ConfigureAwait(false);
                return Any();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                return Reject(e);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static async Task<Outcome<Nop>> Of(Func<Task> asyncAction)
        {
            _ = asyncAction ?? throw new ArgumentNullException(nameof(asyncAction));

            try
            {
                await asyncAction().ConfigureAwait(false);
                return Any();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Reject(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static Outcome<Nop> Of(Action action) 
        {
            _ = action ?? throw new ArgumentNullException(nameof(action));


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


        /// <summary>
        /// Returns an outcome, that returns "nothing". Useful in situations 
        /// where you have conditionals that need to be chained and don't have 
        /// any other Outcome-returning initiation function.
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

        [Obsolete("Use Outcome.Of(Action) instead.")]
        public static Outcome<Nop> Any(Action action) => Of(action);

    }
}
