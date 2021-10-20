using Codoxide.OutcomeInternals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class MapAllExtensions
    {
        /// <summary>
        /// Applies the given function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The function to be applied.</param>
        /// <returns>The enumerable of results.</returns>
        public static Outcome<IEnumerable<TResult>> MapAll<T, TResult>(
            this Outcome<IEnumerable<T>> @this, 
            Func<TResult> fn)
        {
            if (!@this.IsSuccessful)
            {
                return Outcome<IEnumerable<TResult>>
                    .Reject(@this.FailureOrThrow());
            }

            return Outcome.Of(
                () => @this
                    .ResultOrDefault(Enumerable.Empty<T>())
                    .Select(_ => fn())
            );
        }

        /// <summary>
        /// Applies the given function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The function to be applied.</param>
        /// <returns>The enumerable of results.</returns>
        public static Outcome<IEnumerable<TResult>> MapAll<T, TResult>(
            this Outcome<IEnumerable<T>> @this, 
            Func<T, TResult> fn)
        {
            if (!@this.IsSuccessful)
            {
                return Outcome<IEnumerable<TResult>>
                    .Reject(@this.FailureOrThrow());
            }

            return Outcome.Of(
                () => @this
                    .ResultOrDefault(Enumerable.Empty<T>())
                    .Select(r => fn(r))
            );
        }

        /// <summary>
        /// Applies the given async function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The async function to be applied.</param>
        /// <returns>The enumerable of results.</returns>
        public static async Task<Outcome<IEnumerable<TResult>>> MapAll<T, TResult>(
            this Outcome<IEnumerable<T>> @this, 
            Func<Task<TResult>> fn)
        {
            if (@this.IsSuccessful)
            {
                var source = @this.ResultOrDefault(Enumerable.Empty<T>());
                int count = source is ICollection c ? c.Count : 5;
                var dest = new List<TResult>(count);

                return await Utility.Try<IEnumerable<TResult>>(async () => {
                    foreach (var item in source)
                    {
                        dest.Add(await fn());
                    }
                    return dest;
                });
            }

            return @this.FailureOrThrow();
        }

        /// <summary>
        /// Applies the given async function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The async function to be applied.</param>
        /// <returns>The enumerable of results.</returns>
        public static async Task<Outcome<IEnumerable<TResult>>> MapAll<T, TResult>(
            this Outcome<IEnumerable<T>> @this, 
            Func<T, Task<TResult>> fn)
        {
            if (@this.IsSuccessful)
            {
                var source = @this.ResultOrDefault(Enumerable.Empty<T>());
                int count = source is ICollection
                            ? ((ICollection)source).Count
                            : 5;
                var dest = new List<TResult>(count);

                return await Utility.Try<IEnumerable<TResult>>(async () => {
                    foreach (var item in source)
                    {
                        dest.Add(await fn(item));
                    }
                    return dest;
                });
            }

            return @this.FailureOrThrow();
        }

        /// <summary>
        /// Applies the given async function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The async function to be applied.</param>
        /// <returns>The enumerable of results.</returns>
        public static async Task<Outcome<IEnumerable<TResult>>> MapAll<T, TResult>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Func<Task<TResult>> fn)
        {
            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (!@this.IsSuccessful)
                {
                    Outcome<IEnumerable<TResult>>
                        .Reject(@this.FailureOrThrow());
                }

                var source = @this.ResultOrDefault(Enumerable.Empty<T>());
                int count = source is ICollection c ? c.Count : 5;
                var dest = new List<TResult>(count);

                foreach (var _ in source)
                {
                    dest.Add(await fn());
                }
                return new Outcome<IEnumerable<TResult>>(dest);
            });
        }

        /// <summary>
        /// Applies the given async function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The async function to be applied.</param>
        /// <returns>The enumerable of results.</returns>
        public static async Task<Outcome<IEnumerable<TResult>>> MapAll<T, TResult>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Func<T, Task<TResult>> fn)
        {
            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (!@this.IsSuccessful)
                {
                    Outcome<IEnumerable<TResult>>
                        .Reject(@this.FailureOrThrow());
                }

                var source = @this.ResultOrDefault(Enumerable.Empty<T>());
                int count = source is ICollection c ? c.Count : 5;
                var dest = new List<TResult>(count);

                foreach (var item in source)
                {
                    dest.Add(await fn(item));
                }
                return new Outcome<IEnumerable<TResult>>(dest);
            });
        }

        /// <summary>
        /// Applies the given function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The function to be applied.</param>
        /// <returns>The enumerable of restuls.</returns>
        public static async Task<Outcome<IEnumerable<TResult>>> MapAll<T, TResult>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Func<TResult> fn)
        {
            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (!@this.IsSuccessful)
                {
                    Outcome<IEnumerable<TResult>>
                        .Reject(@this.FailureOrThrow());
                }

                var source = @this.ResultOrDefault(Enumerable.Empty<T>());
                int count = source is ICollection c ? c.Count : 5;
                var dest = new List<TResult>(count);

                foreach (var _ in source)
                {
                    dest.Add(fn());
                }
                return new Outcome<IEnumerable<TResult>>(dest);
            });
        }

        /// <summary>
        /// Applies the given function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="TResult">The return type of the generator function.</typeparam>
        /// <typeparam name="TParam">The element type of the input enumerable.</typeparam>
        /// <param name="parameters">The input enumerable.</param>
        /// <param name="generator">The function to be applied.</param>
        /// <returns>The enumerable of restuls.</returns>
        public static async Task<Outcome<IEnumerable<TResult>>> MapAll<T, TResult>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Func<T, TResult> fn)
        {

            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (!@this.IsSuccessful)
                {
                    Outcome<IEnumerable<TResult>>
                        .Reject(@this.FailureOrThrow());
                }

                var source = @this.ResultOrDefault(Enumerable.Empty<T>());
                int count = source is ICollection c ? c.Count : 5;
                var dest = new List<TResult>(count);

                foreach (var item in source)
                {
                    dest.Add(fn(item));
                }
                return new Outcome<IEnumerable<TResult>>(dest);
            });
        }
    }
}
