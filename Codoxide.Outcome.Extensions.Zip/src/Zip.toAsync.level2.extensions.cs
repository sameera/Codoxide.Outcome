using System;
using System.Threading.Tasks;
using Codoxide.Outcomes;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeZipToAsyncExtensions
    {
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<Task<T3>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
            
            var (second, secondFailure) = await Outcome.Of(fn);
            return (secondFailure != null) 
                ? Outcome<(T1, T2, T3)>.Reject(secondFailure)
                : Outcome.Of((first.Item1, first.Item2, second));
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<T1, T2, Task<T3>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
            
            var (second, secondFailure) = await Outcome.Of(() => fn(first.Item1, first.Item2));
            return (secondFailure != null) 
                ? Outcome<(T1, T2, T3)>.Reject(secondFailure)
                : Outcome.Of((first.Item1, first.Item2, second));
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<Task<Outcome<T3>>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
            
            var (second, secondFailure) = await Flatten(Try(fn));
            return (secondFailure != null) 
                ? Outcome<(T1, T2, T3)>.Reject(secondFailure)
                : Outcome.Of((first.Item1, first.Item2, second));
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Outcome<(T1, T2)> @this, Func<T1, T2, Task<Outcome<T3>>> fn)
        {
            var (first, firstFailure) = @this;
            if (!@this.IsSuccessful) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
            
            var (second, secondFailure) = await Flatten(Try(() => fn(first.Item1, first.Item2)));
            return (secondFailure != null) 
                ? Outcome<(T1, T2, T3)>.Reject(secondFailure)
                : Outcome.Of((first.Item1, first.Item2, second));
        }
    }
}
