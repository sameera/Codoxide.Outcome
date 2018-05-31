using System;

namespace Codoxide
{
    using System.Threading.Tasks;
    using static FixedOutcomes;

    static partial class OutcomeExtensions
    {
        public static Outcome<T> ThenTry<T>(this Outcome<T> outcome, Action action)
        {
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

        public static Outcome<T> ThenTry<T>(this Outcome<T> outcome, Action<T> action)
        {
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

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<Outcome<ResultType>> fn)
        {
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
        
        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<ResultType> fn)
        {
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

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<T, ResultType> fn)
        {
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

        public static async Task<Outcome<T>> ThenTry<T>(this Outcome<T> outcome, Func<Task> action)
        {
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

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<Task<Outcome<ResultType>>> fn)
        {
            if (outcome.IsSuccessful)
            {
                return await fn();
            }
            else
            {
                return new Outcome<ResultType>(outcome.Failure);
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<T, Task<Outcome<ResultType>>> fn)
        {
            if (outcome.IsSuccessful)
            {
                return await fn(outcome.Result);
            }
            else
            {
                return new Outcome<ResultType>(outcome.Failure);
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<Task<ResultType>> fn)
        {
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

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> outcome, Func<T, Task<ResultType>> fn)
        {
            if (!outcome.IsSuccessful) new Outcome<ResultType>(outcome.Failure);

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
    }
}
