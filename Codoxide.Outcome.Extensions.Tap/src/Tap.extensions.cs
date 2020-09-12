using System;

namespace Codoxide
{
    using static Codoxide.Internals.Utility;

    public static class OutcomeTapExtensions
    {
        public static Outcome<T> Tap<T>(this Outcome<T> @this, Action action)
        {
            if (!@this.IsSuccessful) return @this;

            return Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> Tap<T>(this Outcome<T> @this, Action<T> action)
        {
            if (!@this.IsSuccessful) return @this;

            return Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}
