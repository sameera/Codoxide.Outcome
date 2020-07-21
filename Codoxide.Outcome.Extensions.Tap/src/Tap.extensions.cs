using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

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

        internal static Outcome<T> Try<T>(Func<Outcome<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return Fail(ex);
            }
        }
    }
}
