using Codoxide.OutcomeInternals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class ForEachExtensions
    {
        /// <summary>
        /// Performs the given function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static Outcome<IEnumerable<T>> ForEach<T>(
            this Outcome<IEnumerable<T>> @this, 
            Action fn)
        {
            if (@this.IsSuccessful)
            {
                foreach (var _ in @this.ResultOrDefault(Enumerable.Empty<T>()))
                {
                    fn();
                }
            }

            return @this;
        }

        /// <summary>
        /// Performs the given function on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static Outcome<IEnumerable<T>> ForEach<T>(
            this Outcome<IEnumerable<T>> @this, 
            Action<T> fn)
        {
            if (@this.IsSuccessful)
            {
                foreach (var item in @this.ResultOrDefault(Enumerable.Empty<T>()))
                {
                    fn(item);
                }
            }

            return @this;
        }

        /// <summary>
        /// Perfors the given async operation on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The async operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(
            this Outcome<IEnumerable<T>> @this, 
            Func<Task> fn)
        {
            if (@this.IsSuccessful)
            {
                foreach (var _ in @this.ResultOrDefault(Enumerable.Empty<T>()))
                {
                    await fn();
                }
            }

            return @this;
        }

        /// <summary>
        /// Perfors the given async operation on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The async operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(
            this Outcome<IEnumerable<T>> @this, 
            Func<T, Task> fn)
        {
            if (@this.IsSuccessful)
            {
                foreach (var item in @this.ResultOrDefault(Enumerable.Empty<T>()))
                {
                    await fn(item);
                }
            }

            return @this;
        }

        /// <summary>
        /// Perfors the given operation on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Action fn)
        {
            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (@this.IsSuccessful)
                {
                    foreach (var _ in @this.ResultOrDefault(Enumerable.Empty<T>()))
                    {
                        fn();
                    }
                }

                return @this;
            });
        }

        /// <summary>
        /// Perfors the given operation on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Action<T> fn)
        {

            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (@this.IsSuccessful)
                {
                    foreach (var item in @this.ResultOrDefault(Enumerable.Empty<T>()))
                    {
                        fn(item);
                    }
                }

                return @this;
            });
        }

        /// <summary>
        /// Perfors the given async operation on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The async operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Func<Task> fn)
        {
            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (@this.IsSuccessful)
                {
                    foreach (var _ in @this.ResultOrDefault(Enumerable.Empty<T>()))
                    {
                        await fn();
                    }
                }

                return @this;
            });
        }

        /// <summary>
        /// Perfors the given async operation on all elements of the input enumerable and
        /// returns an enumerable of the results.
        /// </summary>
        /// <typeparam name="T">The element type of the input enumerable.</typeparam>
        /// <param name="@this">The input enumerable.</param>
        /// <param name="fn">The async operation to be performed.</param>
        /// <returns>The input enumerable.</returns>
        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(
            this Task<Outcome<IEnumerable<T>>> asyncOutcome, 
            Func<T, Task> fn)
        {

            return await Utility.Try(async () => {
                var @this = await asyncOutcome;

                if (@this.IsSuccessful)
                {
                    foreach (var item in @this.ResultOrDefault(Enumerable.Empty<T>()))
                    {
                        await fn(item);
                    }
                }

                return @this;
            });
        }
    }
}
