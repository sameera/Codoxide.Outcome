using Codoxide.Outcomes;

namespace Codoxide.OutcomeExtensions.Filters
{
    public class ExpectationFailure : KnownFailure
    {
        protected internal ExpectationFailure() : base("An expected condition was not met.", 101)
        {
        }

        protected internal ExpectationFailure(Failure another) : base(another)
        {
        }
    }

    internal class ExpectationFailure<T> : ExpectationFailure
    {
        internal T ResultAtSource { get; }

        internal ExpectationFailure(T sourceResult)
        {
            this.ResultAtSource = sourceResult;
        }

        internal ExpectationFailure(T sourceResult, Failure another) : base(another)
        {
            this.ResultAtSource = sourceResult;
        }
    }
}