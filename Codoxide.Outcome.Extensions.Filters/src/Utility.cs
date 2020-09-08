namespace Codoxide.OutcomeExtensions.Filters
{
    internal class Utility
    {
        public static bool IsUnprocessable<T>(Outcome<T> outcome) =>
            !outcome.IsSuccessful && !(outcome.FailureOrThrow() is ExpectationFailure<T>);
    }
}