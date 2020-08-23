using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    public static class OutcomeTapPredicateExtensions
    {
        public static Outcome<T> Tap<T>(this Outcome<T> @this, Func<bool> predicate, Action action)
        {
            if (!@this.IsSuccessful || !predicate()) return @this;

            return Utility.Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> Tap<T>(this Outcome<T> @this, Func<bool> predicate, Action<T> action)
        {
            if (!@this.IsSuccessful || !predicate()) return @this;

            return Utility.Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }

        public static Outcome<T> Tap<T>(this Outcome<T> @this, Func<T, bool> predicate, Action action)
        {
            if (!@this.IsSuccessful || !predicate(@this.ResultOrThrow())) return @this;

            return Utility.Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> Tap<T>(this Outcome<T> @this, Func<T, bool> predicate, Action<T> action)
        {
            if (!@this.IsSuccessful || !predicate(@this.ResultOrThrow())) return @this;

            return Utility.Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}
