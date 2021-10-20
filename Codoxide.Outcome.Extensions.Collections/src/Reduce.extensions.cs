using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class ReduceExtension
    {
        public static Outcome<TResult> Reduce<T, TResult>(
            this Outcome<IEnumerable<T>> @this,
            Func<TResult, T, Outcome<TResult>> generator)
        {
            if (!@this.IsSuccessful)
            {
                return Outcome<TResult>.Reject(@this.FailureOrThrow());
            }

            var parameters = @this.ResultOrDefault();
            TResult previous = default;
            foreach (var param in parameters)
            {
                var current = generator(previous, param);
                if (current.IsSuccessful)
                {
                    previous = current.ResultOrDefault();
                }
                else
                {
                    return Outcome<TResult>.Reject(
                        $"{nameof(generator)} function failed for the parameter value {param}");
                }
            }
            return previous;
        }
        public static async Task<Outcome<TResult>> Reduce<T, TResult>(
            this Task<Outcome<IEnumerable<T>>> input,
            Func<TResult, T, Task<Outcome<TResult>>> generator)
        {
            Outcome<IEnumerable<T>> @this;

            try
            {
                @this = await input;
            }
            catch (Exception ex)
            {
                return Outcome<TResult>.Reject(ex);

            }

            if (!@this.IsSuccessful)
            {
                return Outcome<TResult>.Reject(@this.FailureOrThrow());
            }

            var parameters = @this.ResultOrDefault();
            TResult previous = default;
            foreach (var param in parameters)
            {
                var current = await generator(previous, param);
                if (current.IsSuccessful)
                {
                    previous = current.ResultOrDefault();
                }
                else
                {
                    return Outcome<TResult>.Reject(
                        $"{nameof(generator)} function failed for the parameter value {param}");
                }
            }
            return previous;
        }
    }
}
