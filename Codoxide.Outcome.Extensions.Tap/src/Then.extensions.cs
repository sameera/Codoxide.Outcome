using System;

namespace Codoxide
{
    using static Codoxide.OutcomeInternals.Utility;

    public static class OutcomeThenExtensions
    {
        [Obsolete("Use 'Tap' instead")]
		public static Outcome<T> Then<T>(this Outcome<T> @this, Action action)
        {
            if (!@this.IsSuccessful) return @this;

            return Try(() => {
                action();
                return @this;
            });
        }

        [Obsolete("Use 'Tap' instead")]
		public static Outcome<T> Then<T>(this Outcome<T> @this, Action<T> action)
        {
            if (!@this.IsSuccessful) return @this;

            return Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}
