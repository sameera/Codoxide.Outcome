using System;
using System.Collections.Generic;
using System.Linq;

namespace Codoxide
{
    public static class OutcomeCollectionExtensions
    {
        public static Outcome<IEnumerable<ResultType>> ThenForEach<T, ResultType>(this Outcome<IEnumerable<T>> @this, Func<ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<IEnumerable<ResultType>>.Reject(@this.Failure);

            return Outcome.Of(() => @this.Result.Select(_ => fn()));
        }

        public static Outcome<IEnumerable<ResultType>> ThenForEach<T, ResultType>(this Outcome<IEnumerable<T>> @this, Func<T, ResultType> fn)
        {
            if (!@this.IsSuccessful) return Outcome<IEnumerable<ResultType>>.Reject(@this.Failure);

            return Outcome.Of(() => @this.Result.Select(r => fn(r)));
        }

        public static Outcome<IEnumerable<T>> ThenForEach<T>(this Outcome<IEnumerable<T>> @this, Action fn)
        {
            if (@this.IsSuccessful)
            {
                foreach (var item in @this.Result)
                {
                    fn();
                }
            }

            return @this;
        }

        public static Outcome<IEnumerable<T>> ThenForEach<T>(this Outcome<IEnumerable<T>> @this, Action<T> fn)
        {
            if (@this.IsSuccessful)
            {
                foreach (var item in @this.Result)
                {
                    fn(item);
                }
            }

            return @this;
        }
    }
}
