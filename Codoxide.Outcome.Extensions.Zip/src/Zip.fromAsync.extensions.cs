using System;
using System.Threading.Tasks;
using Codoxide.Outcomes;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeZipFromAsyncExtensions
    {
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<Task<T2>> fn)
            => await (await asyncOutcome).Zip(fn);

        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<T1, Task<T2>> fn)
            => await (await asyncOutcome).Zip(fn);
        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<Task<Outcome<T2>>> fn)
            => await (await asyncOutcome).Zip(fn);

        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<T1, Task<Outcome<T2>>> fn)
            => await (await asyncOutcome).Zip(fn);
        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<T2> fn)
            => (await asyncOutcome).Merge(fn);

        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<T1, T2> fn)
            => (await asyncOutcome).Merge(fn);
        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<Outcome<T2>> fn)
            => (await asyncOutcome).Merge(fn);

        
        public static async Task<Outcome<(T1, T2)>> Zip<T1, T2>(this Task<Outcome<T1>> asyncOutcome, Func<T1, Outcome<T2>> fn)
            => (await asyncOutcome).Merge(fn);
    }
}
