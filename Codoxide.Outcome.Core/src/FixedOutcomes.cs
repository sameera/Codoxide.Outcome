using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class FixedOutcomes
    {
        public static Failure Fail(string reason) => new Failure(reason);

        public static Failure Fail(string reason, int failureCode) => new Failure(reason, failureCode);

        public static Failure Fail(string reason, Exception exception) => new Failure(reason, exception);

        public static Failure Fail(string reason, Exception exception, int failureCode) => new Failure(reason, exception, failureCode);

        public static Failure Fail(Exception exception) => new Failure(exception.Message, exception);

        public static (T result, Exception exception) Try<T>(Func<T> func)
        {
            try
            {
                return (func(), null);
            }
            catch (Exception ex)
            {
                return (default(T), ex);
            }
        }
    }
}
