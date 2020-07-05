using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class FailFailuresExtensions
    {
        public static Outcome<T> Fail<T>(this Outcome<T> @this, Func<Failure> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn());
        }

        public static Outcome<T> Fail<T>(this Outcome<T> @this, Func<T, Failure> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn(@this.ResultOrDefault()));
        }

        public static async Task<Outcome<T>> Fail<T>(this Outcome<T> @this, Func<Task<Failure>> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn());
        }

        public static async Task<Outcome<T>> Fail<T>(this Outcome<T> @this, Func<T, Task<Failure>> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn(@this.ResultOrDefault()));
        }

        public static async Task<Outcome<T>> Fail<T>(this Task<Outcome<T>> promise, Func<Failure> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn());
        }

        public static async Task<Outcome<T>> Fail<T>(this Task<Outcome<T>> promise, Func<T, Failure> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn(@this.ResultOrDefault()));
        }

        public static async Task<Outcome<T>> Fail<T>(this Task<Outcome<T>> promise, Func<Task<Failure>> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn());
        }

        public static async Task<Outcome<T>> Fail<T>(this Task<Outcome<T>> promise, Func<T, Task<Failure>> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn(@this.ResultOrDefault()));
        }
    }
}
