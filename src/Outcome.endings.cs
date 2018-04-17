using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    static partial class OutcomeExtensions
    {
        public static void EndWith<T>(this Outcome<T> outcome, Action<T> onSuccess = null, Action<Failure> onFailure = null)
        {
            if (outcome.IsSuccessful)
            {
                onSuccess(outcome.Result);
            }
            else
            {
                onFailure(outcome.Failure);
            }
        }

        public static ReturnType Return<T, ReturnType>(this Outcome<T> outcome, Func<T, ReturnType> onSuccess = null, Func<Failure, ReturnType> onFailure = null)
        {
            if (outcome.IsSuccessful)
            {
                return onSuccess(outcome.Result);
            }
            else
            {
                return onFailure(outcome.Failure);
            }
        }

        public static async Task EndWith<T>(this Task<Outcome<T>> asyncOutcome, Action<T> onSuccess = null, Action<Failure> onFailure = null)
        {
            var outcome = await asyncOutcome;
            outcome.EndWith(onSuccess, onFailure);
        }

        public static async Task<ReturnType> Return<T, ReturnType>(this Task<Outcome<T>> asyncOutcome, Func<T, ReturnType> onSuccess = null, Func<Failure, ReturnType> onFailure = null)
        {
            var outcome = await asyncOutcome;
            return outcome.Return(onSuccess, onFailure);
        }
    }
}

//namespace Codoxide
//{
//    using static FixedOutcomes;

//    public class SafeExecutionContext<T>
//    {
//        private readonly Outcome<T> _outcome;

//        public void Reject(string reason)
//        {
//            _outcome.Failure = new Outcomes.Failure(reason);
//        }

//        public void Try(Action action)
//        {
//            try
//            {
//                action();
//            }
//            catch (Exception ex)
//            {
//                _outcome.Failure = Fail(ex);
//            }
//        }

//        internal SafeExecutionContext(Outcome<T> outcome)
//        {
//            _outcome = outcome;
//        }
//    }

//    partial class Outcome<T>
//    {
//        //public ResultType Fulfill<ResultType>(Func<T, ResultType> func)
//        //{
//        //    if (this.IsSuccessful) return func(this.Result);

//        //    return default(ResultType);
//        //}

//        public Outcome<T> Then(Action<SafeExecutionContext<T>> action)
//        {
//            if (this.IsSuccessful)
//            {
//                action(new SafeExecutionContext<T>(this));
//            }
//            return this;
//        }
//    }
//}
