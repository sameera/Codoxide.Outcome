using System;
using System.Threading.Tasks;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeL4AsyncExtensions
    {
        public static async Task<Outcome<(T1, T2, T3, T4, T5)>> Merge<T1, T2, T3, T4, T5>(
                this Task<Outcome<(T1, T2, T3, T4)>> @this, 
                Func<T5> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3, T4, T5)>.Reject(failure);

                return (result.Item1, result.Item2, result.Item3, result.Item4, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4, T5)>> Merge<T1, T2, T3, T4, T5>(
                this Task<Outcome<(T1, T2, T3, T4)>> @this, 
                Func<T1, T2, T3, T4, T5> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3, T4, T5)>.Reject(failure);

                return (
                    result.Item1, result.Item2, result.Item3, result.Item4, 
                    fn(result.Item1, result.Item2, result.Item3, result.Item4)
                );
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4, T5)>> Merge<T1, T2, T3, T4, T5>(
            this Task<Outcome<(T1, T2, T3, T4)>> @this, 
            Func<T1, T2, T3, T4, Task<T5>> fn
        )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3, T4, T5)>.Reject(failure);

                return (
                    result.Item1, result.Item2, result.Item3, result.Item4, 
                    await fn(result.Item1, result.Item2, result.Item3, result.Item4)
                );
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4, T5)>> Merge<T1, T2, T3, T4, T5>(
                this Task<Outcome<(T1, T2, T3, T4)>> @this, 
                Func<Outcome<T5>> fn
            )
        {
            try
            {
                var (result, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3, T4, T5)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, result.Item4, secondResult) 
                    : Outcome<(T1, T2, T3, T4, T5)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public async static Task<Outcome<(T1, T2, T3, T4, T5)>> Merge<T1, T2, T3, T4, T5>(
                this Task<Outcome<(T1, T2, T3, T4)>> @this, 
                Func<T1, T2, T3, T4, Outcome<T5>> fn
            )
        {
            try
            {
                var (result, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3, T4, T5)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn(result.Item1, result.Item2, result.Item3, result.Item4);
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, result.Item4, secondResult) 
                    : Outcome<(T1, T2, T3, T4, T5)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public async static Task<Outcome<(T1, T2, T3, T4, T5)>> Merge<T1, T2, T3, T4, T5>(
            this Task<Outcome<(T1, T2, T3, T4)>> @this, 
            Func<T1, T2, T3, T4, Task<Outcome<T5>>> fn
        )
        {
            try
            {
                var (result, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3, T4, T5)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = await fn(result.Item1, result.Item2, result.Item3, result.Item4);
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, result.Item4, secondResult) 
                    : Outcome<(T1, T2, T3, T4, T5)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
