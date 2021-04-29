using System;
using System.Threading.Tasks;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeL3AsyncExtensions
    {
        public static async Task<Outcome<(T1, T2, T3, T4)>> Merge<T1, T2, T3, T4>(
                this Task<Outcome<(T1, T2, T3)>> @this, 
                Func<T4> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3, T4)>.Reject(failure);

                return (result.Item1, result.Item2, result.Item3, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4)>> Merge<T1, T2, T3, T4>(
                this Task<Outcome<(T1, T2, T3)>> @this, 
                Func<T1, T2, T3, T4> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3, T4)>.Reject(failure);

                return (result.Item1, result.Item2, result.Item3, fn(result.Item1, result.Item2, result.Item3));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4)>> Merge<T1, T2, T3, T4>(
            this Task<Outcome<(T1, T2, T3)>> @this, 
            Func<T1, T2, T3, Task<T4>> fn
        )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3, T4)>.Reject(failure);

                return (
                    result.Item1, result.Item2, result.Item3, 
                    await fn(result.Item1, result.Item2, result.Item3)
                );
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4)>> Merge<T1, T2, T3, T4>(
                this Task<Outcome<(T1, T2, T3)>> @this, 
                Func<Outcome<T4>> fn
            )
        {
            try
            {
                var (result, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3, T4)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, secondResult) 
                    : Outcome<(T1, T2, T3, T4)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4)>> Merge<T1, T2, T3, T4>(
                this Task<Outcome<(T1, T2, T3)>> @this, 
                Func<T1, T2, T3, Outcome<T4>> fn
            )
        {
            try
            {
                var (result, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3, T4)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn(result.Item1, result.Item2, result.Item3);
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, secondResult) 
                    : Outcome<(T1, T2, T3, T4)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3, T4)>> Merge<T1, T2, T3, T4>(
            this Task<Outcome<(T1, T2, T3)>> @this, 
            Func<T1, T2, T3, Task<Outcome<T4>>> fn
        )
        {
            try
            {
                var (result, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3, T4)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = await fn(result.Item1, result.Item2, result.Item3);
                return (secondFailure == null) 
                    ? (result.Item1, result.Item2, result.Item3, secondResult) 
                    : Outcome<(T1, T2, T3, T4)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
