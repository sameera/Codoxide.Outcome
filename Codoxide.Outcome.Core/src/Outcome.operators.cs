using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Codoxide
{
    using static FixedOutcomes;

    partial struct Outcome<T>
    {
        public static implicit operator Outcome<T>(T result) => new Outcome<T>(result);
        
        public static explicit operator T(Outcome<T> outcome) => outcome.ResultOrDefault();

        public static implicit operator Outcome<T>(Failure failure) => new Outcome<T>(failure);

        public static implicit operator Outcome<T>(Exception exception) => new Outcome<T>(Fail(exception));

        public static implicit operator Outcome<T>((T result, Failure failure) outcome) => new Outcome<T>(outcome.result, outcome.failure);

        public static implicit operator Outcome<T>((T result, Exception exception) tuple) => new Outcome<T>(tuple.result, Fail(tuple.exception));

        public static implicit operator ValueTuple<T, Failure>(Outcome<T> outcome) => (outcome.Result, outcome._failure);

        public static bool operator==(Outcome<T> left, Outcome<T> right) => left.Equals(right);
        public static bool operator !=(Outcome<T> left, Outcome<T> right) => !left.Equals(right);

        public override bool Equals(object obj)
        {
            return obj is Outcome<T> outcome &&
                   IsSuccessful == outcome.IsSuccessful &&
                   EqualityComparer<Failure>.Default.Equals(_failure, outcome._failure) &&
                   EqualityComparer<T>.Default.Equals(Result, outcome.Result);
        }

        public override int GetHashCode()
        {
            int hashCode = 1785519654;
            hashCode = hashCode * -1521134295 + IsSuccessful.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Failure>.Default.GetHashCode(_failure);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Result);
            return hashCode;
        }
    }
}
