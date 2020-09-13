using System;
using System.Threading.Tasks;

namespace Codoxide.OutcomeExtensions.Filters
{
    using static Codoxide.OutcomeInternals.Utility;

    public struct Switchable<T>
    {
        public T Result { get; }

        internal Switchable(T result)
        {
            Result = result;
        }

        public (bool, Func<Outcome<TResult>>) When<TResult>(bool condition, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return (condition, () => func(precedent));
        }

        public (bool, Func<Outcome<TResult>>) When<TResult>(T match, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var result = this.Result;

            return (Equals(Result, match), () => func(Outcome.Of(result)));
        }

        public (bool, Func<Task<Outcome<TResult>>>) When<TResult>(T match, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return (
                Equals(Result, match), 
                async () => await func(precedent).ConfigureAwait(false)
            );
        }

        public (bool, Func<Outcome<TResult>>) When<TResult>(object match, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return (
                Equals(Result, match),
                () => func(precedent)
            );
        }

        public (bool, Func<Outcome<TResult>>) When<TResult>(Func<T, bool> predicate, Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            var result = Result;
            
            return (
                predicate != null && predicate(result), 
                () => func(precedent)
            );
        }

        public (bool, Func<Outcome<TResult>>) Otherwise<TResult>(Func<Outcome<T>, Outcome<TResult>> func)
        {
            var precedent = Outcome.Of(Result);
            return (true, () => func(precedent));
        }

        // ************
        // Async
        // ************

        public (bool, Func<Task<Outcome<TResult>>>) When<TResult>(bool condition, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return (condition, async () => await func(precedent).ConfigureAwait(false));
        }

        public (bool, Func<Task<Outcome<TResult>>>) When<TResult>(object match, Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return (Equals(Result, match), async () => await func(precedent).ConfigureAwait(false));
        }

        public (bool, Func<Task<Outcome<TResult>>>) Otherwise<TResult>(Func<Outcome<T>, Task<Outcome<TResult>>> func)
        {
            var precedent = Outcome.Of(Result);
            return (true, async () => await func(precedent).ConfigureAwait(false));
        }
        
        public static implicit operator T(Switchable<T> source) => source.Result;
    }
}
