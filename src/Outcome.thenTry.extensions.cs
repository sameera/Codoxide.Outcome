using System;

namespace Codoxide
{
    using System.Threading.Tasks;
    using static FixedOutcomes;

    public static class OutcomeThenTryExtensions
    {
        private static Outcome<ResultType> Try<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn)
        {
            try
            {
                return fn();
            }
            catch (Exception x)
            {
                return Outcome<ResultType>.Reject(Fail(x));
            }
        }

        public static Outcome<T> ThenTry<T>(this Outcome<T> @this, Action action) => @this.Try(() => @this.Then(action));

        public static Outcome<T> ThenTry<T>(this Outcome<T> @this, Action<T> action) => @this.Try(() => @this.Then(action));

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> @this, Func<Outcome<ResultType>> fn) => @this.Try(() => @this.Then(fn));

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> @this, Func<ResultType> fn) => @this.Try(() => @this.Then(fn));

        public static Outcome<ResultType> ThenTry<T, ResultType>(this Outcome<T> @this, Func<T, ResultType> fn) => @this.Try(() => @this.Then(fn));

        public static async Task<Outcome<T>> ThenTry<T>(this Outcome<T> @this, Func<Task> action)
        {
            if (@this.IsSuccessful)
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

            return @this;
        }       

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<Task<Outcome<ResultType>>> fn)
        {
            if (@this.IsSuccessful)
            {
                return await fn();
            }
            else
            {
                return new Outcome<ResultType>(@this.Failure);
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<T, Task<Outcome<ResultType>>> fn)
        {
            if (@this.IsSuccessful)
            {
                return await fn(@this.Result);
            }
            else
            {
                return new Outcome<ResultType>(@this.Failure);
            }
        }

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<Task<ResultType>> fn)
        {
            if (!@this.IsSuccessful) new Outcome<ResultType>(@this.Failure);

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

        public static async Task<Outcome<ResultType>> ThenTry<T, ResultType>(this Outcome<T> @this, Func<T, Task<ResultType>> fn)
        {
            if (!@this.IsSuccessful) new Outcome<ResultType>(@this.Failure);

            try
            {
                var result = await fn(@this.Result);
                return new Outcome<ResultType>(result);
            }
            catch (Exception ex)
            {
                return new Outcome<ResultType>(Fail(ex));
            }
        }
    }
}
