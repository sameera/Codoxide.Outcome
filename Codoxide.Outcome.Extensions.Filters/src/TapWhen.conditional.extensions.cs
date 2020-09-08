using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;
    using static Codoxide.Internals.Utility;

    public static partial class TapWhenExtensions
    {
        public static Outcome<T> TapWhen<T>(this Outcome<T> @this, bool condition, Action action)
        {
            if (!condition || !@this.IsSuccessful) return @this;

            return Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> TapWhen<T>(this Outcome<T> @this, bool condition, Action<T> action)
        {
            if (!condition || !@this.IsSuccessful) return @this;

            return Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}