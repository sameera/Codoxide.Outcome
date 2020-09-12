namespace Codoxide
{
    public static class OutcomeTaskExtensions
    {
        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action action)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) action();

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Action<T> action)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) action(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<T, Task> asyncAction)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) await asyncAction(outcome.ResultOrDefault());

                return outcome;
            });
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<ResultType> func) //where ResultType: class
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return Outcome.Of(func);

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<Outcome<ResultType>> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func();

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, ResultType> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ResultType>(func(outcome.ResultOrDefault()));

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ResultType>> Then<T, ResultType>(this Task<Outcome<T>> asyncPromise, Func<T, Outcome<ResultType>> func)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return func(outcome.ResultOrDefault());

                return Outcome<ResultType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task> asyncAction)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) await asyncAction();

                return outcome;
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<ReturnType>> asyncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc());

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<T, Task<ReturnType>> asyncFunc) 
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return new Outcome<ReturnType>(await asyncFunc(outcome.ResultOrDefault()));

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<T>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return outcome;
            });
        }

        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<Outcome<T>> asyncPromise, Func<Task<Outcome<ReturnType>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc();

                return Outcome<ReturnType>.Reject(outcome.FailureOrNull());
            });
        }

        public static async Task<Outcome<ReturnValue>> Then<T, ReturnValue>(this Task<Outcome<T>> asyncPromise, Func<T, Task<Outcome<ReturnValue>>> aysncFunc)
        {
            return await Try(async () => {
                var outcome = await asyncPromise;
                if (outcome.IsSuccessful) return await aysncFunc(outcome.ResultOrDefault());

                return Outcome<ReturnValue>.Reject(outcome.FailureOrNull());
            });
        }

        private static async Task<Outcome<T>> Try<T>(Func<Task<Outcome<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return Outcome<T>.Reject(Fail(ex));
            }
        }
    }
}
