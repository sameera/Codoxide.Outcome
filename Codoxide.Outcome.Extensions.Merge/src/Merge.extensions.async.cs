using System;
using System.Threading.Tasks;
using static Codoxide.FixedOutcomes;

namespace Codoxide
{
    public static partial class OutcomeMergeAsyncExtensions
    {
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
                this Task<Outcome<T1>> @this, 
                Func<T2> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2)>.Reject(failure);

                return (result, fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
            this Task<Outcome<T1>> @this, 
            Func<Task<T2>> fn
        )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2)>.Reject(failure);

                return (result, await fn());
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
                this Task<Outcome<T1>> @this, 
                Func<T1, T2> fn
            )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2)>.Reject(failure);

                return (result, fn(result));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
            this Task<Outcome<T1>> @this, 
            Func<T1, Task<T2>> fn
        )
        {
            try
            {
                var (result, failure) = await @this;
                if (failure != null) return Outcome<(T1, T2)>.Reject(failure);

                return (result, await fn(result));
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
                this Task<Outcome<T1>> @this, 
                Func<Outcome<T2>> fn
            )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn();
                return (secondFailure == null) 
                    ? (first, secondResult) 
                    : Outcome<(T1, T2)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
            this Task<Outcome<T1>> @this, 
            Func<Task<Outcome<T2>>> fn
        )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = await fn();
                return (secondFailure == null) 
                    ? (first, secondResult) 
                    : Outcome<(T1, T2)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
                this Task<Outcome<T1>> @this, 
                Func<T1, Outcome<T2>> fn
            )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = fn(first);
                return (secondFailure == null) 
                    ? (first, secondResult) 
                    : Outcome<(T1, T2)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
        
        public static async Task<Outcome<(T1, T2)>> Merge<T1, T2>(
            this Task<Outcome<T1>> @this, 
            Func<T1, Task<Outcome<T2>>> fn
        )
        {
            try
            {
                var (first, firstFailure) = await @this;
                if (firstFailure != null) return Outcome<(T1, T2)>.Reject(firstFailure);
                
                var (secondResult, secondFailure) = await fn(first);
                return (secondFailure == null) 
                    ? (first, secondResult) 
                    : Outcome<(T1, T2)>.Reject(secondFailure);
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
