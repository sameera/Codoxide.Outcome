//using System;
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
