using System;

using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeL3Extensions
    {
        public static Outcome<(T1, T2, T3, T4, T5)> Merge<T1, T2, T3, T4, T5>(
                this Outcome<(T1, T2, T3, T4)> @this, 
                Func<T5> fn
            )
        {
            var (result, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4, T5)>.Reject(failure);

            return Try<T1, T2, T3, T4, T5>(result, fn);
        }
        
        public static Outcome<(T1, T2, T3, T4, T5)> Merge<T1, T2, T3, T4, T5>(
                this Outcome<(T1, T2, T3, T4)> @this, 
                Func<T1, T2, T3, T4, T5> fn
            )
        {
            var (result, failure) = @this;
            
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4, T5)>.Reject(failure);
                
            return Try<T1, T2, T3, T4, T5>(
                result, 
                () => fn(result.Item1, result.Item2, result.Item3, result.Item4));
        }
        
        public static Outcome<(T1, T2, T3, T4, T5)> Merge<T1, T2, T3, T4, T5>(
                this Outcome<(T1, T2, T3, T4)> @this, 
                Func<Outcome<T5>> fn
            )
        {
            var (result, resultFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4, T5)>.Reject(resultFailure);
            
            try
            {
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, result.Item4, secondResult) 
                    : Outcome<(T1, T2, T3, T4, T5)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static Outcome<(T1, T2, T3, T4, T5)> Merge<T1, T2, T3, T4, T5>(
                this Outcome<(T1, T2, T3, T4)> @this, 
                Func<T1, T2, T3, T4, Outcome<T5>> fn
            )
        {
            var (result, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4, T5)>.Reject(failure);

            try
            {
                var (secondResult, secondFailure) = fn(result.Item1, result.Item2, result.Item3, result.Item4);
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, result.Item4, secondResult) 
                    : Outcome<(T1, T2, T3, T4, T5)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        private static Outcome<(T1, T2, T3, T4, T5)> Try<T1, T2, T3, T4, T5>(
                (T1, T2, T3, T4) result, 
                Func<T5> fn
            )
        {
            try
            {
                return (result.Item1, result.Item2, result.Item3, result.Item4, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
