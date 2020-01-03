﻿//using Codoxide.Outcomes;
//using System;
//using System.Threading.Tasks;

//namespace Codoxide
//{
//    public static class OutcomeConditionalThenElseExtensions
//    {
//        public static Outcome<T> Then<T>(this Outcome<T> @this, bool condition, Action action, Action otherwise) =>
//            (condition) ? @this.Then(action) : @this.Then(otherwise);

//        public static Outcome<T> Then<T>(this Outcome<T> @this, bool condition, Action<T> action, Action otherwise) =>
//            condition ? @this.Then(r => action(r)) : @this.Then(otherwise);

//        public static async Task<Outcome<T>> Then<T>(this Outcome<T> @this, bool condition, Func<Task> action, Func<Task> otherwise) =>
//            condition ? await @this.Then(action) : await @this.Then(otherwise);

//        public static async Task<Outcome<T>> Then<T>(this Outcome<T> @this, bool condition, Func<T, Task> action, Func<T, Task> otherwise) =>
//            condition ? await @this.Then(action) : await @this.Then(otherwise);

//        public static Outcome<T> Then<T>(this Outcome<T> @this, bool condition, Func<T> fn, Func<T> otherwise) =>
//            condition ? @this.Then(fn) : @this.Then(otherwise);

//        public static Outcome<T> Then<T>(this Outcome<T> @this, bool condition, Func<Outcome<T>> fn, Func<Outcome<T>> otherwise) =>
//            condition ? @this.Then(fn) : @this.Then(otherwise);

//        public static Outcome<T> Then<T>(this Outcome<T> @this, bool condition, Func<Failure> fn) =>
//            condition ? @this.Then(() => Outcome<T>.Reject(fn())) : @this;

//        public static Outcome<T> Then<T>(this Outcome<T> @this, bool condition, Func<ValueTuple<T, Failure>> fn) =>
//            condition ? @this.Then(() => fn()) : @this;

//        public static Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Action action) =>
//            condition ? @this.Then(action) : @this;

//        public static Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Action<T> action) =>
//            condition ? @this.Then(r => action(r)) : @this;

//        public static Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Func<Task> action) =>
//            condition ? @this.Then(action) : @this;

//        public static Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Func<T, Task> action) =>
//            condition ? @this.Then(r => action(r)) : @this;

//        public static Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Func<Task<T>> fn) =>
//            condition ? @this.Then(fn) : @this;

//        public static Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Func<T, Task<T>> fn) =>
//            condition ? @this.Then(r => fn(r)) : @this;

//        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Func<Task<Failure>> fn)
//        {
//            if (!condition) return await @this;

//            var failure = await fn();
//            return Outcome<T>.Reject(failure);
//        }

//        public static async Task<Outcome<T>> Then<T>(this Task<Outcome<T>> @this, bool condition, Func<T, Task<Failure>> fn)
//        {
//            var outcome = await @this;

//            if (!condition) return outcome;

//            return await outcome
//                .Then(result => fn(result))
//                .Then(failure => (Outcome<T>)failure);
//        }
//    }
//}
