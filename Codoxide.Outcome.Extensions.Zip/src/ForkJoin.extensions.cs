using System;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Codoxide.Outcomes;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeZipAsyncExtensions
    {
        public static async Task<Outcome<(T1, T2)>> ForkJoin<T1, T2>(this Task<Outcome<T1>> @this, Func<Task<Outcome<T2>>> fn)
        {
            var other = fn();
            await Task.WhenAll(@this, other);

            var (first, f1) = await @this;
            var (second, f2) = await other;

            return (f1, f2, first, second) switch {
                _ when f1 != null && f2 != null => Outcome<(T1, T2)>.Reject(new AggregateFailure(f1, f2)),
                              _ when f1 != null => Outcome<(T1, T2)>.Reject(new AggregateFailure(f1)),
                                              _ => (first, second)
            };
        }

        public static async Task<Outcome<(T1, T2)>> ForkJoin<T1, T2>(this Task<Outcome<T1>> @this, Func<Task<T2>> fn)
        {
            Task<T2> other = Task.FromResult(default(T2)!);
            Failure? f2 = null;
            
            try
            {
                other = fn();
                await Task.WhenAll(@this, other);     
            }
            catch (Exception e)
            {
                f2 = new Failure(e.Message, e);
            }

            var (first, f1) = await @this;
            var second = await other;
            
            return (f1, f2, first, second) switch {
                _ when f1 != null && f2 != null => Outcome<(T1, T2)>.Reject(new AggregateFailure(f1, f2)),
                              _ when f1 != null => Outcome<(T1, T2)>.Reject(new AggregateFailure(f1)),
                                              _ => (first, second)
            };
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> ForkJoin<T1, T2, T3>(this Task<Outcome<(T1, T2)>> @this, Func<Task<Outcome<T3>>> fn)
        {
            var other = fn();
            await Task.WhenAll(@this, other);

            var (first, f1) = await @this;
            var (second, f2) = await other;

            return (f1, f2, first, second) switch {
                var (e1, e2, _, _) when e1 != null && e2 != null => Outcome<(T1, T2, T3)>.Reject(new AggregateFailure(e1, e2)),
                var (e1, _, _, _)  when e1 != null => Outcome<(T1, T2, T3)>.Reject(new AggregateFailure(e1)),
                _ => (first.Item1, first.Item2, second)
            };    
        }

        public static async Task<Outcome<(T1, T2, T3)>> ForkJoin<T1, T2, T3>(this Task<Outcome<(T1, T2)>> @this, Func<Task<T3>> fn)
        {
            Task<T3> other = Task.FromResult(default(T3)!);
            Failure? f2 = null;
            
            try
            {
                other = fn();
                await Task.WhenAll(@this, other);     
            }
            catch (Exception e)
            {
                f2 = new Failure(e.Message, e);
            }

            var (first, f1) = await @this;
            var second = await other;
            
            return (f1, f2, first, second) switch {
                _ when f1 != null && f2 != null => Outcome<(T1, T2, T3)>.Reject(new AggregateFailure(f1, f2)),
                _ when f1 != null => Outcome<(T1, T2, T3)>.Reject(new AggregateFailure(f1)),
                _ => (first.Item1, first.Item2, second)
            };
        }
    }
}
