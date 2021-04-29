using System;

using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeL2Extensions
    {
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(
                this Outcome<(T1, T2)> @this, 
                Func<T3> fn
            )
        {
            var (result, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(failure);

            return Try<T1, T2, T3>(result, fn);
        }
        
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(
                this Outcome<(T1, T2)> @this, 
                Func<T1, T2, T3> fn
            )
        {
            var (result, failure) = @this;
            return !@this.IsSuccessful 
                ? Outcome<(T1, T2, T3)>.Reject(failure) 
                : Try<T1, T2, T3>(result, () => fn(result.Item1, result.Item2));
        }
        
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(
                this Outcome<(T1, T2)> @this, 
                Func<Outcome<T3>> fn
            )
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
            
            try
            {
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2, secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(
                this Outcome<(T1, T2)> @this, 
                Func<T1, T2, Outcome<T3>> fn
            )
        {
            var (first, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(failure);

            try
            {
                var (secondResult, secondFailure) = fn(first.Item1, first.Item2);
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2, secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }

        private static Outcome<(T1, T2, T3)> Try<T1, T2, T3>(
                (T1, T2) result, 
                Func<T3> fn
            )
        {
            try
            {
                var (r1, r2) = result;
                return (r1, r2, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
