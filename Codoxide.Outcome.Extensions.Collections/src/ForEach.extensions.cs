using Codoxide.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class ForEachExtensions
    {
        public static Outcome<IEnumerable<T>> ForEach<T>(this Outcome<IEnumerable<T>> @this, Action fn)
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

        public static Outcome<IEnumerable<T>> ForEach<T>(this Outcome<IEnumerable<T>> @this, Action<T> fn)
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

        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(this Outcome<IEnumerable<T>> @this, Func<Task> fn)
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

        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(this Outcome<IEnumerable<T>> @this, Func<T, Task> fn)
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

        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(this Task<Outcome<IEnumerable<T>>> asyncOutcome, Action fn)
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

        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(this Task<Outcome<IEnumerable<T>>> asyncOutcome, Action<T> fn)
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

        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(this Task<Outcome<IEnumerable<T>>> asyncOutcome, Func<Task> fn)
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

        public static async Task<Outcome<IEnumerable<T>>> ForEach<T>(this Task<Outcome<IEnumerable<T>>> asyncOutcome, Func<T, Task> fn)
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
