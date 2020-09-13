using Codoxide.Outcomes;
using System;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace Codoxide
{
    public readonly partial struct Outcome<T> : IOutcome<T>, IOutcome
    {
        public bool IsSuccessful => _failure == null;

        private readonly Failure _failure;

        internal readonly T Result;

        public Outcome(in T result) : this(result, null) { }

        internal Outcome(Failure failure) : this(default, failure) { }

        private Outcome(in T result, Failure failure)
        {
            if (result is IOutcome outcome)
            {
                this._failure = outcome.FailureOrNull();

                if (!outcome.IsSuccessful || outcome.ResultOrDefault() == null)
                {
                    this.Result = default;
                }
                else
                {
                    var innerResult = outcome.ResultOrDefault();
                    if (innerResult is T assignableResult)
                    {
                        this.Result = assignableResult;
                    }
                    else
                    {
                        throw new InvalidCastException($"Cannot cast from {innerResult.GetType().Name} to {typeof(T).Name}");
                    }
                }
            }
            else
            {
                Result = result;
                _failure = failure;
            }
        }

        public Failure FailureOrNull() => this._failure;

        public Failure FailureOrThrow() => this._failure ?? throw new InvalidOperationException("There is no failure as this is a successful Outcome.");

        public T ResultOrDefault() => this.IsSuccessful ? this.Result : default;

        object IOutcome.ResultOrDefault() => this.ResultOrDefault();

        public T ResultOrDefault(T defaultValue) => this.IsSuccessful ? this.Result : defaultValue;

        public T ResultOrThrow() => this.IsSuccessful ? this.Result : throw this._failure.AsException();

        object IOutcome.ResultOrThrow() => this.ResultOrThrow();
        
        public void Deconstruct(out T result, out Failure failure)
        {
            failure = this.FailureOrNull();
            result = this.ResultOrDefault();
        }

    }
}
