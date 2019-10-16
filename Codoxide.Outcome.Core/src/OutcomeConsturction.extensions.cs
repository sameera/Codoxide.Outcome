//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Codoxide.Outcome
//{
//    public static class OutcomeConsturctionExtensions
//    {
//        public static Outcome<T> AsOutcome<T>(this T @this) => new Outcome<T>(@this);
        
//        public static Task<Outcome<T>> AsAsyncOutcome<T>(this T @this) => Task.FromResult(new Outcome<T>(@this));
//    }
//}
