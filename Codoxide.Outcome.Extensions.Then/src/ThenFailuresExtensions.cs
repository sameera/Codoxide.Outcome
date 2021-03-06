﻿namespace Codoxide.Extensions
{
    public static class ThenFailuresExtensions
    {
        public static Outcome<T> Then<T>(this Outcome<T> @this, Func<Failure> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn());
        }

        public static Outcome<T> Then<T>(this Outcome<T> @this, Func<T, Failure> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn(@this.ResultOrDefault()));
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> @this, Func<Task<Failure>> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn());
        }

        public static async Task<Outcome<T>> Then<T>(this Outcome<T> @this, Func<T, Task<Failure>> fn)
        {
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn(@this.ResultOrDefault()));
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> promise, Func<Failure> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn());
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> promise, Func<T, Failure> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(fn(@this.ResultOrDefault()));
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> promise, Func<Task<Failure>> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn());
        }

        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> promise, Func<T, Task<Failure>> fn)
        {
            var @this = await promise;
            if (!@this.IsSuccessful) return @this;

            return Outcome<T>.Reject(await fn(@this.ResultOrDefault()));
        }
    }
}
