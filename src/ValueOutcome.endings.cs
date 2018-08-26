//using Codoxide.Outcomes;
//using System;
//using System.Threading.Tasks;

//namespace Codoxide
//{
//    static partial class ValueOutcomeExtensions
//    {
//        public static void EndWith<T>(this (T result, Failure failure) outcome, Action<T> onSuccess = null, Action<Failure> onFailure = null)
//        {
//            if (outcome.failure == null && onSuccess != null)
//            {
//                onSuccess(outcome.result);
//            }
//            else if (outcome.failure != null && onFailure != null)
//            {
//                onFailure(outcome.failure);
//            }
//        }

//        public static void EndWith<T>(this (T result, Failure failure) outcome, Action onSuccess = null, Action<Failure> onFailure = null)
//        {
//            outcome.EndWith(r => onSuccess(), onFailure);
//        }

//        public static ReturnType Return<T, ReturnType>(this (T result, Failure failure) outcome, Func<T, ReturnType> onSuccess = null, Func<Failure, ReturnType> onFailure = null)
//        {
//            if (outcome.failure == null && onSuccess != null)
//            {
//                return onSuccess(outcome.result);
//            }
//            else if (outcome.failure != null && onFailure != null)
//            {
//                return onFailure(outcome.failure);
//            }
//            else
//            {
//                return default(ReturnType);
//            }
//        }

//        public static async Task EndWith<T>(this Task<(T result, Failure failure)> asyncOutcome, Action<T> onSuccess = null, Action<Failure> onFailure = null)
//        {
//            var outcome = await asyncOutcome;
//            outcome.EndWith(onSuccess, onFailure);
//        }

//        public static async Task<ReturnType> Return<T, ReturnType>(this Task<(T result, Failure failure)> asyncOutcome, Func<T, ReturnType> onSuccess = null, Func<Failure, ReturnType> onFailure = null)
//        {
//            var outcome = await asyncOutcome;
//            return outcome.Return(onSuccess, onFailure);
//        }
//    }
//}
