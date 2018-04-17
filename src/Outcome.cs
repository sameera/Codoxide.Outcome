using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    using static FixedOutcomes;

    public sealed partial class Outcome<T>
    {
        public bool IsSuccessful { get { return Failure == null; } }

        internal Failure Failure { get; set; }

        internal T Result { get; }

        public Outcome(T result) : this(result, null)
        {
        }

        private Outcome(Failure failure) : this(default(T))
        {
            Failure = failure;
        }

        //public Outcome<T> Then(Action onSuccess = null, Action<Failure> onFailure = null)
        //{
        //    if (this.IsSuccessful && onSuccess != null)
        //    {
        //        onSuccess();
        //    }
        //    else if (!this.IsSuccessful && onFailure != null)
        //    {
        //        onFailure(this.Failure);
        //    }
        //    return this;
        //}

        //public Outcome<T> Then(Action<T> onSuccess = null, Action<Failure> onFailure = null)
        //{
        //    if (this.IsSuccessful && onSuccess != null)
        //    {
        //        onSuccess(this.Result);
        //    }
        //    else if (!this.IsSuccessful && onFailure != null)
        //    {
        //        onFailure(this.Failure);
        //    }
        //    return this;
        //}

        //public Outcome<T> Then(Func<T, T> onSuccess = null, Action<Failure> onFailure = null)
        //{
        //    if (this.IsSuccessful && onSuccess != null)
        //    {
        //        return onSuccess(this.Result);
        //    }
        //    else if (!this.IsSuccessful && onFailure != null)
        //    {
        //        onFailure(this.Failure);
        //    }
        //    return this;
        //}

        //public Outcome<T> Then(Func<T, T> onSuccess = null, Func<Failure, T> onFailure = null)
        //{
        //    if (this.IsSuccessful && onSuccess != null)
        //    {
        //        return onSuccess(this.Result);
        //    }
        //    else if (this.IsSuccessful)
        //    {
        //        return this;
        //    }
        //    else if (onFailure != null)
        //    {
        //        return onFailure(this.Failure);
        //    }
        //    else
        //    {
        //        return this;
        //    }
        //}

        private Outcome(T result, Failure failure)
        {
            this.Result = result;
            Failure = failure;
        }

        public Failure FailureOrDefault() => this.IsSuccessful ? null : this.Failure;

        public T ResultOrDefault() => this.IsSuccessful ? this.Result : default(T);

        public static Outcome<T> Reject(string reason)
        {
            return new Outcome<T>(new Failure(reason));
        }

        public static Outcome<T> Reject(string reason, Exception exception)
        {
            return new Outcome<T>(new Failure(reason, exception));
        }

        internal static Outcome<T> Reject(Failure failure)
        {
            return new Outcome<T>(default(T), failure);
        }
        public static implicit operator Outcome<T>(T result) => new Outcome<T>(result);

        public static implicit operator Outcome<T>(Failure failure) => new Outcome<T>(failure);

        public static implicit operator Outcome<T>(Exception exception) => new Outcome<T>(Fail(exception));

        public static implicit operator Outcome<T>((T result, Failure failure) outcome) => new Outcome<T>(outcome.result, outcome.failure);


    }
}
