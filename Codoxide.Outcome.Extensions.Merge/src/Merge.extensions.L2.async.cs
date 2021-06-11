using System;
using System.Threading.Tasks;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeL2AsyncExtensions
    {
        public static async Task<Outcome<(T1, T2, T3)>> Merge<T1, T2, T3>(
                this Task<Outcome<(T1, T2)>> @this, 
                Func<T3> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3)>.Reject(failure);

                return (result.Item1, result.Item2, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> Merge<T1, T2, T3>(
                this Task<Outcome<(T1, T2)>> @this, 
                Func<T1, T2, T3> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3)>.Reject(failure);

                return (result.Item1, result.Item2, fn(result.Item1, result.Item2));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> Merge<T1, T2, T3>(
            this Task<Outcome<(T1, T2)>> @this, 
            Func<T1, T2, Task<T3>> fn
        )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2, T3)>.Reject(failure);

                return (result.Item1, result.Item2, await fn(result.Item1, result.Item2));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2, T3)>> Merge<T1, T2, T3>(
                this Task<Outcome<(T1, T2)>> @this, 
                Func<Outcome<T3>> fn
            )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2, secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public async static Task<Outcome<(T1, T2, T3)>> Merge<T1, T2, T3>(
                this Task<Outcome<(T1, T2)>> @this, 
                Func<T1, T2, Outcome<T3>> fn
            )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn(first.Item1, first.Item2);
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2 , secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public async static Task<Outcome<(T1, T2, T3)>> Merge<T1, T2, T3>(
            this Task<Outcome<(T1, T2)>> @this, 
            Func<T1, T2, Task<Outcome<T3>>> fn
        )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2, T3)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = await fn(first.Item1, first.Item2);
                return (secondFailure == null) 
                    ? (first.Item1, first.Item2 , secondResult) 
                    : Outcome<(T1, T2, T3)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
