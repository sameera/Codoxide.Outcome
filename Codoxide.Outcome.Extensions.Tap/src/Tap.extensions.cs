using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    public static class OutcomeTapExtensions
    {
        public static Outcome<T> Tap<T>(this Outcome<T> @this, Action action)
        {
            if (!@this.IsSuccessful) return @this;

            return Utility.Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> Tap<T>(this Outcome<T> @this, Action<T> action)
        {
            if (!@this.IsSuccessful) return @this;

            return Utility.Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}
