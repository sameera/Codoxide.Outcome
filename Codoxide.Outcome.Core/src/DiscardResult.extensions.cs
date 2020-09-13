using System.Threading.Tasks;
using Codoxide.Internals;

namespace Codoxide
{
    public static class DiscardResultExtensions
    {
        public static Outcome<Nop> DiscardResult<T>(this Outcome<T> @this)
            => @this.IsSuccessful ? Outcome.Any() : Outcome.Reject(@this.FailureOrThrow());

        public static async Task<Outcome<Nop>> DiscardResult<T>(this Task<Outcome<T>> asyncOutcome)
            => await Utility.Try(async () => {
                var @this = await asyncOutcome.ConfigureAwait(false);
                return DiscardResult(@this);
            }).ConfigureAwait(false);
    }
}