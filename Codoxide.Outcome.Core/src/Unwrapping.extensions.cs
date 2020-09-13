using Codoxide.Outcomes;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class UnwrappingExtensions
    {
        public static async Task<T> ResultOrDefault<T>(this Task<Outcome<T>> @this)
            => (await @this.ConfigureAwait(false)).ResultOrDefault();

        public static async Task<T> ResultOrThrow<T>(this Task<Outcome<T>> @this)
            => (await @this.ConfigureAwait(false)).ResultOrThrow();

        public static async Task<Failure> FailureOrNull<T>(this Task<Outcome<T>> @this)
            => (await @this.ConfigureAwait(false)).FailureOrNull();

        public static async Task<Failure> FailureOrThrow<T>(this Task<Outcome<T>> @this)
            => (await @this.ConfigureAwait(false)).FailureOrThrow();

        public static Task<Outcome<T>> ForAsync<T>(this Outcome<T> @this) => Task.FromResult(@this);
    }
}
