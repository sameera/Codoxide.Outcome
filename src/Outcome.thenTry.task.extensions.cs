using System;

namespace Codoxide
{
    using System.Threading.Tasks;
    using static FixedOutcomes;

    static partial class OutcomeExtensions
    {
        public static async Task<Outcome<T>> ThenTry<T>(this Task<Outcome<T>> asyncPromise, Action action)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    return new Outcome<T>(Fail(ex));
                }
            }

            return outcome;
        }

        public static async Task<Outcome<T>> ThenTry<T>(this Task<Outcome<T>> asyncPromise, Action<T> action)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful)
            {
                try
                {
                    action(outcome.Result);
                }
                catch (Exception ex)
                {
                    return new Outcome<T>(Fail(ex));
                }
            }

            return outcome;
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Outcome<ResultType>> fn)
        {
            var outcome = await asyncPromise;
            if (!outcome.IsSuccessful) return new Outcome<ResultType>(outcome.Failure);

            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                return new Outcome<ResultType>(Fail(ex));
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> fn)
        {
            var outcome = await asyncPromise;
            if (!outcome.IsSuccessful) return new Outcome<ResultType>(outcome.Failure);

            try
            {
                return new Outcome<ResultType>(fn());
            }
            catch (Exception ex)
            {
                return new Outcome<ResultType>(Fail(ex));
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> fn)
        {
            var outcome = await asyncPromise;
            if (!outcome.IsSuccessful) return new Outcome<ResultType>(outcome.Failure);

            try
            {
                return new Outcome<ResultType>(fn(outcome.Result));
            }
            catch (Exception ex)
            {
                return new Outcome<ResultType>(Fail(ex));
            }
        }

        public static async Task<Outcome<T>> ThenTry<T>(this Task<Outcome<T>> asyncPromise, Func<Task> action)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful)
            {
                try
                {
                    await action();
                }
                catch (Exception ex)
                {
                    return new Outcome<T>(Fail(ex));
                }
            }

            return outcome;
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ResultType>>> fn)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful)
            {
                return await fn();
            }
            else
            {
                return new Outcome<ResultType>(outcome.Failure);
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ResultType>>> fn)
        {
            var outcome = await asyncPromise;
            if (outcome.IsSuccessful)
            {
                return await fn(outcome.Result);
            }
            else
            {
                return new Outcome<ResultType>(outcome.Failure);
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<ResultType>> fn)
        {
            var outcome = await asyncPromise;
            if (!outcome.IsSuccessful) return new Outcome<ResultType>(outcome.Failure);

            try
            {
                var result = await fn(outcome.Result);
                return new Outcome<ResultType>(result);
            }
            catch (Exception ex)
            {
                return new Outcome<ResultType>(Fail(ex));
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Task<ResultType>> fn)
        {
            var outcome = await asyncPromise;
            if (!outcome.IsSuccessful) new Outcome<ResultType>(outcome.Failure);

            try
            {
                var result = await fn();
                return new Outcome<ResultType>(result);
            }
            catch (Exception ex)
            {
                return new Outcome<ResultType>(Fail(ex));
            }
        }
    }
}
