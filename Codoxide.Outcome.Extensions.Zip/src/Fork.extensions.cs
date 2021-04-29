using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static partial class ForkExtensions
    {
        public static (Task<Outcome<T1>>, Task<Outcome<T2>>) 
            Fork<T1, T2>(this Task<Outcome<T1>> @this, Func<Task<Outcome<T2>>> other)
            => (@this, other());

        public static async Task<Outcome<T>> Join<T1, T2, T>(
                this (Task<Outcome<T1>>, Task<Outcome<T2>>) @this,
                Func<T1, T2, T> joiner
            )
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await @this.Item1;
                var (t2, f2) = await @this.Item2;

                bool hasErrors = f1 != null || f2 != null;
                AggregateFailure? failure = hasErrors ? new AggregateFailure(f1!, f2!) : null;

                return hasErrors ? Outcome<T>.Reject(failure) : Outcome.Of(joiner(t1, t2));
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(new AggregateFailure(e));
            }            
        }

        public static async Task<Outcome<T>> Join<T1, T2, T>(
                this (Task<Outcome<T1>>, Task<Outcome<T2>>) @this,
                Func<T1, T2, AggregateFailure?, T> joiner
            )
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await @this.Item1;
                var (t2, f2) = await @this.Item2;

                AggregateFailure? failure = (f1 != null || f2 != null) ? new AggregateFailure(f1, f2) : null;

                return Outcome.Of(joiner(t1, t2, failure));
            }
            catch (Exception e)
            {
                return Outcome.Of(joiner(default!, default!, new AggregateFailure(e)));
            }
        }
    }
}