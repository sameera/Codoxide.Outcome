using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class ForkExtensions
    {
        public static (Task<Outcome<T1>>, Task<Outcome<T2>>, Task<Outcome<T3>>) 
            Fork<T1, T2, T3>(this (Task<Outcome<T1>>, Task<Outcome<T2>>) @this, Func<Task<Outcome<T3>>> other)
            => (@this.Item1, @this.Item2, other());

        public static async Task<Outcome<T>> Join<T1, T2, T3, T>(
                this (Task<Outcome<T1>>, Task<Outcome<T2>>, Task<Outcome<T3>>) @this,
                Func<T1, T2, T3, T> joiner
            )
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await @this.Item1;
                var (t2, f2) = await @this.Item2;
                var (t3, f3) = await @this.Item3;

                bool hasErrors = f1 != null || f2 != null || f3 != null;
                AggregateFailure? failure = hasErrors ? new AggregateFailure(f1!, f2!, f3!) : null;

                return hasErrors ? Outcome<T>.Reject(failure) : Outcome.Of(joiner(t1, t2, t3));
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(new AggregateFailure(e));
            }            
        }

        public static async Task<Outcome<T>> Join<T1, T2, T3, T>(
                this (Task<Outcome<T1>>, Task<Outcome<T2>>, Task<Outcome<T3>>) @this,
                Func<T1, T2, T3, AggregateFailure?, T> joiner
            )
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await @this.Item1;
                var (t2, f2) = await @this.Item2;
                var (t3, f3) = await @this.Item3;

                AggregateFailure? failure = (f1 != null || f2 != null || f3 != null) ? new AggregateFailure(f1, f2, f3) : null;

                return Outcome.Of(joiner(t1, t2, t3, failure));
            }
            catch (Exception e)
            {
                return Outcome.Of(joiner(default!, default!, default!, new AggregateFailure(e)));
            }
        }
    }
}