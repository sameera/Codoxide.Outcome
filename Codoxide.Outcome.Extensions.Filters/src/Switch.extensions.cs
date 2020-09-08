//using Codoxide.OutcomeExtensions.Filters;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Codoxide
//{
//    public static class SwitchExtensions
//    {
//        public static Outcome<TResult> Switch<T, TResult>(this Outcome<T> @this, params Func<Outcome<T>, Outcome<TResult>>[] branches)
//        {
//            if (!@this.IsSuccessful) return Outcome<TResult>.Reject(@this.FailureOrThrow());

//            for (int i = 0; i < branches.Length; i++)
//            {
//                var outcome = branches[i](@this);
//                if (outcome.IsSuccessful) return outcome;
//            }

//            return new ExpectationFailure<T>(default);
//        }
//    }
//}
