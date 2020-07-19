using Codoxide.Outcomes;

namespace Codoxide
{
    internal interface IOutcome<out T>
    {
        bool IsSuccessful { get; }
        Failure FailureOrNull();
        Failure FailureOrThrow();
        T ResultOrDefault();
        T ResultOrThrow();
    }

    internal interface IOutcome
    {
        bool IsSuccessful { get; }
        Failure FailureOrNull();
        Failure FailureOrThrow();
        object ResultOrDefault();
        object ResultOrThrow();
    }
}