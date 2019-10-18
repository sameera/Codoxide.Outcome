//using System;
//using System.Threading.Tasks;

//namespace Codoxide
//{
//    public static partial class OutcomeConditionalExtensions
//    {
//        // Conditionals should support Actions only. If the conditionals supported returning values
//        // it makes the rest fo the flow confusing.

//        public static Outcome<T> ThenIf<T>(this Outcome<T> outcome, bool condition, Action action)
//        {
//            if (outcome.IsSuccessful && condition) action();

//            return outcome;
//        }

//        [Obsolete("Don't use functions")]
//        public static Outcome<T> ThenIf<T>(this Outcome<T> outcome, bool condition, Func<Outcome<T>> fn)
//        {
//            if (outcome.IsSuccessful && condition) return fn();

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Outcome<T> outcome, bool condition, Func<Task> fn)
//        {
//            if (outcome.IsSuccessful && condition) await fn();

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Outcome<T> outcome, bool condition, Func<T, Task> fn)
//        {
//            if (outcome.IsSuccessful && condition) await fn(outcome.Result);

//            return outcome;
//        }

//        public static Outcome<T> ThenIf<T>(this Outcome<T> outcome, Predicate<T> condition, Action action)
//        {
//            if (outcome.IsSuccessful && condition(outcome.Result)) action();

//            return outcome;
//        }

//        public static Outcome<T> ThenIf<T>(this Outcome<T> outcome, Predicate<T> condition, Action<T> action)
//        {
//            if (outcome.IsSuccessful && condition(outcome.Result)) action(outcome.Result);

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Task<Outcome<T>> asyncPromise, bool condition, Action action)
//        {
//            var outcome = await asyncPromise;

//            if (outcome.IsSuccessful && condition) action();

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Task<Outcome<T>> asyncPromise, Predicate<T> condition, Action action)
//        {
//            var outcome = await asyncPromise;

//            if (outcome.IsSuccessful && condition(outcome.Result)) action();

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Task<Outcome<T>> asyncPromise, Predicate<T> condition, Action<T> action)
//        {
//            var outcome = await asyncPromise;

//            if (outcome.IsSuccessful && condition(outcome.Result)) action(outcome.Result);

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Task<Outcome<T>> asyncPromise, Predicate<T> condition, Func<Task> action)
//        {
//            var outcome = await asyncPromise;

//            if (outcome.IsSuccessful && condition(outcome.Result)) await action();

//            return outcome;
//        }

//        public static async Task<Outcome<T>> ThenIf<T>(this Task<Outcome<T>> asyncPromise, Predicate<T> condition, Func<T, Task> action)
//        {
//            var outcome = await asyncPromise;

//            if (outcome.IsSuccessful && condition(outcome.Result)) await action(outcome.Result);

//            return outcome;
//        }
//    }
//}
