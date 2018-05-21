using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    public static class GenericOutcomes<T>
    {
        public static (T schema, Failure failure) Error(string message, Exception exception = null)
        {
            return (default(T), FixedOutcomes.Fail(message, exception));
        }

        public static (T schema, Failure failure) Success(T result) => (result, null);
    }
}
