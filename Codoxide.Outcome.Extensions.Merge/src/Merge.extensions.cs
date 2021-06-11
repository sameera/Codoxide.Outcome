using System;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeExtensions
    {
        public static Outcome<(T1, T2)> Merge<T1, T2>(this Outcome<T1> @this, Func<T2> fn)
        {
            var (result, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2)>.Reject(failure);
            
            return Try<(T1, T2)>(() => (result, fn()));
        }
        
        public static Outcome<(T1, T2)> Merge<T1, T2>(this Outcome<T1> @this, Func<T1, T2> fn)
        {
            var (result, failure) = @this;
            return !@this.IsSuccessful ? Outcome<(T1, T2)>.Reject(failure) : Try<(T1, T2)>(() => (result, fn(result)));
        }
        
        public static Outcome<(T1, T2)> Merge<T1, T2>(this Outcome<T1> @this, Func<Outcome<T2>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2)>.Reject(firstFailure);
            
            return Try(() => {
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (first, secondResult) 
                    : Outcome<(T1, T2)>.Reject(firstFailure);
            });
        }
        
        public static Outcome<(T1, T2)> Merge<T1, T2>(this Outcome<T1> @this, Func<T1, Outcome<T2>> fn)
        {
            var (first, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2)>.Reject(failure);
            
            return Try<(T1, T2)>(() => {
                var (secondResult, secondFailure) = fn(first);
                return (secondFailure == null) 
                    ? (first, secondResult) 
                    : Outcome<(T1, T2)>.Reject(failure);
            });
        }
        
        internal static Outcome<T> Try<T>(Func<Outcome<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
