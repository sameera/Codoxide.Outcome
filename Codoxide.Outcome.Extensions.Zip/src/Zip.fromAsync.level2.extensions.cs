using System;
using System.Threading.Tasks;
using Codoxide.Outcomes;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeZipFromAsyncExtensions
    {
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<Task<T3>> fn)
            => await (await asyncOutcome).Zip(fn);
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<T1, T2, Task<T3>> fn)
            => await (await asyncOutcome).Zip(fn);
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<Task<Outcome<T3>>> fn)
            => await (await asyncOutcome).Zip(fn);

        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<T1, T2, Task<Outcome<T3>>> fn)
            => await (await asyncOutcome).Zip(fn);
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<T3> fn)
            => (await asyncOutcome).Merge(fn);
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<T1, T2, T3> fn)
            => (await asyncOutcome).Merge(fn);
        
        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<Outcome<T3>> fn)
            => (await asyncOutcome).Merge(fn);

        public static async Task<Outcome<(T1, T2, T3)>> Zip<T1, T2, T3>(this Task<Outcome<(T1, T2)>> asyncOutcome, Func<T1, T2, Outcome<T3>> fn)
            => (await asyncOutcome).Merge(fn);
    }
}
