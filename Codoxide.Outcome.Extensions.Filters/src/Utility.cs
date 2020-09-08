using System;
using System.Collections.Generic;
using System.Text;

namespace Codoxide.OutcomeExtensions.Filters
{
    class Utility
    {
        public static bool IsUnprocessable<T>(Outcome<T> outcome) =>
            !outcome.IsSuccessful && !(outcome.FailureOrThrow() is ExpectationFailure<T>);
    }
}
