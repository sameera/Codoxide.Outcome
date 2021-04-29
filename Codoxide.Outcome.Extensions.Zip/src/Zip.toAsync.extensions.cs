using System;
using System.Threading.Tasks;
using Codoxide.Outcomes;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeZipToAsyncExtensions
    {
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Outcome<T1> @this, Func<Task<T2>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<ValueTuple<T1, T2>>.Reject(firstFailure);
            
            var (second, secondFailure) = await Outcome.Of(fn);
            return (secondFailure != null) 
                ? Outcome<(T1, T2)>.Reject(secondFailure)
                : Outcome.Of((first, second));
        }
        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Outcome<T1> @this, Func<T1, Task<T2>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<ValueTuple<T1, T2>>.Reject(firstFailure);
            
            var (second, secondFailure) = await Outcome.Of(() => fn(first));
            return (secondFailure != null) 
                ? Outcome<(T1, T2)>.Reject(secondFailure)
                : Outcome.Of((first, second));
        }
        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Outcome<T1> @this, Func<Task<Outcome<T2>>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<ValueTuple<T1, T2>>.Reject(firstFailure);
            
            var (second, secondFailure) = await Flatten(Try(fn));
            return (secondFailure != null) 
                ? Outcome<(T1, T2)>.Reject(secondFailure)
                : Outcome.Of((first, second));
        }
        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Outcome<T1> @this, Func<T1, Task<Outcome<T2>>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<ValueTuple<T1, T2>>.Reject(firstFailure);
            
            var (second, secondFailure) = await Flatten(Try(() => fn(first)));
            return (secondFailure != null) 
                ? Outcome<(T1, T2)>.Reject(secondFailure)
                : Outcome.Of((first, second));
        }
        
        internal static async Task<Outcome<T>> Try<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();
                return Outcome.Of(result);
            }
            catch (Exception ex)
            {
                return Outcome<T>.Reject(ex);
            }
        }

        internal static async Task<(T, Failure)> Flatten<T>(Task<Outcome<Outcome<T>>> asyncOutcome)
        {
            var outcome = await asyncOutcome;
            return !outcome.IsSuccessful 
                ? Outcome<T>.Reject(outcome.FailureOrThrow()) 
                : outcome.ResultOrThrow();
        }
    }
}
