using System;

namespace Codoxide
{
    using Codoxide.Internals;

    public static partial class TapWhenExtensions
    {
        public static Outcome<T> TapWhen<T>(this Outcome<T> @this, Func<bool> predicate, Action action)
        {
            if (!@this.IsSuccessful || !predicate()) return @this;

            return Utility.Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> TapWhen<T>(this Outcome<T> @this, Func<bool> predicate, Action<T> action)
        {
            if (!@this.IsSuccessful || !predicate()) return @this;

            return Utility.Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }

        public static Outcome<T> TapWhen<T>(this Outcome<T> @this, Func<T, bool> predicate, Action action)
        {
            if (!@this.IsSuccessful || !predicate(@this.ResultOrThrow())) return @this;

            return Utility.Try(() => {
                action();
                return @this;
            });
        }

        public static Outcome<T> TapWhen<T>(this Outcome<T> @this, Func<T, bool> predicate, Action<T> action)
        {
            if (!@this.IsSuccessful || !predicate(@this.ResultOrThrow())) return @this;

            return Utility.Try(() => {
                action(@this.ResultOrDefault());
                return @this;
            });
        }
    }
}