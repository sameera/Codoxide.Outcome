using System;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeZipExtensions
    {
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<T1, T2, T3> fn)
        {
            var (first, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(failure);
            
            return Try<(T1, T2, T3)>(() => {
                var second = fn(first.Item1, first.Item2);
                return (first.Item1, first.Item2, second);
            });
        }
        
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<T3> fn)
        {
            var (first, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(failure);
            
            return Try<(T1, T2, T3)>(() => {
                var second = fn();
                return (first.Item1, first.Item2, second);
            });
        }

        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<Outcome<T3>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
            
            return Try(() => {
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2, secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(firstFailure);
            });
        }
        
        public static Outcome<(T1, T2, T3)> Merge<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<T1, T2, Outcome<T3>> fn)
        {
            var (first, failure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(failure);
            
            return Try(() => {
                var (secondResult, secondFailure) = fn(first.Item1, first.Item2);
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2,  secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(failure);
            });
        }
    }
}
