using Codoxide.Outcomes;

namespace Codoxide {
    public interface IOutcome<out T> {
        bool IsSuccessful { get; }
        Failure FailureOrNull();
        Failure FailureOrThrow();
        T ResultOrDefault();
        T ResultOrThrow();   
    }

    public interface IOutcome
    {
         bool IsSuccessful { get; }
         Failure FailureOrNull();
         Failure FailureOrThrow();
         object ResultOrDefault();
         object ResultOrThrow();      
    }
}