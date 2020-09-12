using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    partial class Outcome
    {
        public static Outcome<Nop> Never() => new Outcome<Nop>(new Failure("Intended Failure", IntendedFailureCode));

        public static Outcome<Nop> Reject(string reason) => new Outcome<Nop>(new Failure(reason));

        public static Outcome<Nop> Reject(string reason, Exception exception) => new Outcome<Nop>(new Failure(reason, exception));

        public static Outcome<Nop> Reject(Exception exception) => Reject(exception.Message, exception);

        internal static Outcome<Nop> Reject(Failure failure) => new Outcome<Nop>(failure);
    }

    partial struct Outcome<T>
    {
        public static Outcome<T> Reject(in string reason, int failureCode = Failure.GeneralFailure) =>
            new Outcome<T>(new Failure(reason, failureCode));

        public static Outcome<T> Reject(in string reason, Exception exception, int failureCode = Failure.GeneralFailure) =>
            new Outcome<T>(new Failure(reason, exception, failureCode));

        public static Outcome<T> Reject(Exception exception, int failureCode = Failure.GeneralFailure) =>
            new Outcome<T>(new Failure(exception.Message, exception, failureCode));

        public static Outcome<T> Reject(Failure failure) => new Outcome<T>(default, failure);
    }
}