using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class FixedOutcomes
    {

        public static Failure Fail(string reason) => new Failure(reason);

        public static Failure Fail(string reason, int failureCode) => 
            new Failure(reason, failureCode);

        public static Failure Fail(string reason, Exception exception) => 
            new Failure(reason, exception);

        public static Failure Fail(string reason, Exception exception, int failureCode) => 
            new Failure(reason, exception, failureCode);

        public static Failure Fail(Exception exception) => new Failure(
                exception?.Message ?? 
                    "Unspecified error. This failure is reported when " +
                    "a Failure is returned expecting an expception but " +
                    "the given Exception is actually null.", 
                exception
            );

        public static Outcome<T> Fail<T>(string reason) => Outcome<T>.Reject(reason);
        public static Task<Outcome<T>> FailAsync<T>(string reason) => 
            Outcome<T>.Reject(reason).ForAsync();

        public static (T result, Exception exception) Try<T>(Func<T> func)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));

            try
            {
                return (func(), null);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return (default(T), ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
