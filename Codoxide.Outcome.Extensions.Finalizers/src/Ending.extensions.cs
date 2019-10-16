using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class OutcomeEndingsExtensions
    {
        public static OutcomeFinalizer<T, ReturnType> Return<T, ReturnType>(this Outcome<T> outcome, Func<T, ReturnType> successHandler)
        {
            return new OutcomeFinalizer<T, ReturnType>(outcome).OnSuccess(successHandler);
        }

        public static async Task<OutcomeFinalizer<T, ReturnType>> Return<T, ReturnType>(this Task<Outcome<T>> @this, Func<T, ReturnType> successHandler)
        {
            var outcome = await @this;
            return new OutcomeFinalizer<T, ReturnType>(outcome).OnSuccess(successHandler);
        }

        public static async Task<OutcomeFinalizer<T, ReturnType>> 
            Catch<T, ReturnType, ExceptionType>(
                this Task<OutcomeFinalizer<T, ReturnType>> @this,
                Func<ExceptionType, Task<ReturnType>> handler) where ExceptionType : Exception
        {
            var finalizer = await @this;
            return await finalizer.Catch(handler);
        }

        public static async Task<OutcomeFinalizer<T, ReturnType>> 
            Catch<T, ReturnType>(
                this Task<OutcomeFinalizer<T, ReturnType>> @this, 
                Func<Failure, Task<ReturnType>> handler)
        {
            var finalizer = await @this;
            return await finalizer.Catch(handler);
        }

        public static async Task<OutcomeFinalizer<T, ReturnType>> 
            Catch<T, ReturnType>(
                this Task<OutcomeFinalizer<T, ReturnType>> @this,
                Func<Failure, ReturnType> handler)
        {
            var finalizer = await @this;
            return finalizer.Catch(handler);
        }

        public static async Task<ReturnType> Unwrap<T, ReturnType>(this Task<OutcomeFinalizer<T, ReturnType>> @this)
        {
            var finalizer = await @this;
            return finalizer.Unwrap();
        }
        
    }
}
