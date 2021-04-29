using System;

using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeL3Extensions
    {
        public static Outcome<(T1, T2, T3, T4)> Merge<T1, T2, T3, T4>(
                this Outcome<(T1, T2, T3)> @this, 
                Func<T4> fn
            )
        {
            var (result, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4)>.Reject(failure);

            return Try<T1, T2, T3, T4>(result, fn);
        }
        
        public static Outcome<(T1, T2, T3, T4)> Merge<T1, T2, T3, T4>(
                this Outcome<(T1, T2, T3)> @this, 
                Func<T1, T2, T3, T4> fn
            )
        {
            var (result, failure) = @this;
            
            if (!@this.IsSuccessful) return  Outcome<(T1, T2, T3, T4)>.Reject(failure);
                
            return Try<T1, T2, T3, T4>(
                result, 
                () => fn(result.Item1, result.Item2, result.Item3));
        }
        
        public static Outcome<(T1, T2, T3, T4)> Merge<T1, T2, T3, T4>(
                this Outcome<(T1, T2, T3)> @this, 
                Func<Outcome<T4>> fn
            )
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4)>.Reject(firstFailure);
            
            try
            {
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2, first.Item3, secondResult) 
                    : Outcome<(T1, T2, T3, T4)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static Outcome<(T1, T2, T3, T4)> Merge<T1, T2, T3, T4>(
                this Outcome<(T1, T2, T3)> @this, 
                Func<T1, T2, T3, Outcome<T4>> fn
            )
        {
            var (first, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3, T4)>.Reject(failure);

            try
            {
                var (secondResult, secondFailure) = fn(first.Item1, first.Item2, first.Item3);
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2, first.Item3, secondResult) 
                    : Outcome<(T1, T2, T3, T4)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        private static Outcome<(T1, T2, T3, T4)> Try<T1, T2, T3, T4>(
                (T1, T2, T3) result, 
                Func<T4> fn
            )
        {
            try
            {
                return (result.Item1, result.Item2, result.Item3, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
