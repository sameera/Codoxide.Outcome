using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /**
        /* Async
        /*/

        //public static async Task<Outcome<IEnumerable<ResultType>>> ThenForEach<T, ResultType>(this Task<Outcome<IEnumerable<T>>> outcome, Func<ResultType> fn)
        //{
        //    var @this = await outcome;

        //    if (!@this.IsSuccessful) return Outcome<IEnumerable<ResultType>>.Reject(@this.Failure);

        //    return Outcome.Of(() => @this.Result.Select(_ => fn()));
        //}

        //public static async Task<Outcome<IEnumerable<ResultType>>> ThenForEach<T, ResultType>(this Task<Outcome<IEnumerable<T>>> outcome, Func<T, ResultType> fn)
        //{
        //    var @this = await outcome;

        //    if (!@this.IsSuccessful) return Outcome<IEnumerable<ResultType>>.Reject(@this.Failure);

        //    return Outcome.Of(() => @this.Result.Select(r => fn(r)));
        //}

        //public static async Task<Outcome<IEnumerable<T>>> ThenForEach<T>(this Task<Outcome<IEnumerable<T>>> outcome, Action fn)
        //{
        //    var @this = await outcome;

        //    if (@this.IsSuccessful)
        //    {
        //        foreach (var item in @this.Result)
        //        {
        //            fn();
        //        }
        //    }

        //    return @this;
        //}

        //public static async Task<Outcome<IEnumerable<T>>> ThenForEach<T>(this Task<Outcome<IEnumerable<T>>> outcome, Action<T> fn)
        //{
        //    var @this = await outcome;

        //    if (@this.IsSuccessful)
        //    {
        //        foreach (var item in @this.Result)
        //        {
        //            fn(item);
        //        }
        //    }

        //    return @this;
        //}
    }
}
