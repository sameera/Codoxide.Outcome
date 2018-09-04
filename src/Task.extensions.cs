//using System;
//using System.Threading.Tasks;
//using static Codoxide.FixedOutcomes;

//namespace Codoxide
//{
//    public static partial class TaskExtensions
//    {
//        public static Task<Outcome<T>> Then<T>(this Task<T> @this, Action action) where T: class
//        {
//            return @this

//                .ContinueWith(t => {
//                    action();
//                    return new Outcome<T>(t.Result);
//                }, TaskContinuationOptions.OnlyOnRanToCompletion)

//                .ContinueWith(t => Outcome<T>.Reject(Fail(t.Exception)), TaskContinuationOptions.OnlyOnFaulted);
//        }

//        public static Task<Outcome<T>> Then<T>(this Task<T> @this, Action<T> action) where T: class
//        {
//            return @this

//                .ContinueWith(t => {
//                    action(t.Result);
//                    return new Outcome<T>(t.Result);
//                }, TaskContinuationOptions.OnlyOnRanToCompletion)

//                .ContinueWith(t => Outcome<T>.Reject(Fail(t.Exception)), TaskContinuationOptions.OnlyOnFaulted);
//        }

//        public static Task<Outcome<ResultType>> Then<T, ResultType>(this Task<T> @this, Func<ResultType> func) where T: class
//        {
//            return @this
//                .ContinueWith(t => new Outcome<ResultType>(func), TaskContinuationOptions.OnlyOnRanToCompletion)
//                .ContinueWith(t => Outcome<ResultType>.Reject(Fail(t.Exception)), TaskContinuationOptions.OnlyOnFaulted);
//        }

//        public static Task<Outcome<ResultType>> Then<T, ResultType>(this Task<T> @this, Func<T, ResultType> func) where T: class
//        {
//            return @this
//                .ContinueWith(t => func(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
//                .ContinueWith(t => Outcome<ResultType>.Reject(Fail(t.Exception)), TaskContinuationOptions.OnlyOnFaulted);
//        }

//        public static async Task<Outcome<T>> Then<T>(this Task<T> @this, Func<Task> asyncAction) where T: class
//        {
//            try
//            {
//                var result = await @this.ConfigureAwait(false);
//                await asyncAction().ConfigureAwait(false);
//                return result;
//            }
//            catch (Exception ex)
//            {
//                return Outcome<T>.Reject(Fail(ex));
//            }

//        }

//        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<T> @this, Func<Task<ReturnType>> asyncFunc) where T: class
//        {
//            try
//            {
//                var thisResult = await @this;
//                return await asyncFunc().ConfigureAwait(false);
//            }
//            catch (Exception ex)
//            {
//                return Outcome<ReturnType>.Reject(Fail(ex));
//            }

//        }

//        public static async Task<Outcome<ReturnType>> Then<T, ReturnType>(this Task<T> @this, Func<T, Task<ReturnType>> asyncFunc) where ReturnType: class
//        {
//            try
//            {
//                var result = await @this.ConfigureAwait(false);
//                return await asyncFunc(result);
//            }
//            catch (Exception ex)
//            {
//                return Outcome<ReturnType>.Reject(Fail(ex));
//            }

//        }

//        public static Task<Outcome<T>> GetOutcome<T>(this Task<T> @this) where T: class
//        {
//            return @this
//                .ContinueWith(t => new Outcome<T>(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
//                .ContinueWith(t => Outcome<T>.Reject(FixedOutcomes.Fail(t.Exception)), TaskContinuationOptions.OnlyOnFaulted);
//        }

//        public static Task<Outcome<T>> GetOutcome<T>(this Task<Outcome<T>> @this)
//        {
//            return @this
//                .ContinueWith(t => t.Result, TaskContinuationOptions.OnlyOnRanToCompletion)
//                . With(t => Outcome<T>.Reject(FixedOutcomes.Fail(t.Exception)), TaskContinuationOptions.OnlyOnFaulted);
//        }
//    }
//}
