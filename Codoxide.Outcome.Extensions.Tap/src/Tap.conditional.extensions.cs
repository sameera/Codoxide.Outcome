using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    public static class OutcomeConditionalTapExtensions
    {
        public static Outcome<T> Tap<T>(this Outcome<T> @this, bool condition, Action action)
        {
            if (!condition || !@this.IsSuccessful) return @this;

            return Utility.Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> Tap<T>(this Outcome<T> @this, bool condition, Action<T> action)
        {
            if (!condition || !@this.IsSuccessful) return @this;

            return Utility.Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}
