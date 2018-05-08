using Codoxide.Outcomes;
using System;

namespace Codoxide
{
    public delegate void OutAction<T>(out T output);

    public delegate void OutAction<T, U>(T input, out U output);

    public delegate ResultType OutFunc<OutType, ResultType>(out OutType output);

    public delegate ResultType ParameterziedOutFunc<InputType, OutputType, ResultType>(InputType input, out OutputType output);

    public static class FixedOutcomes
    {
        public static Failure Fail(string reason) => new Failure(reason);

        public static Failure Fail(string reason, Exception exception) => new Failure(reason, exception);

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

    public static class GenericOutcomes<T>
    {
        public static (T schema, Failure failure) Error(string message, Exception exception = null)
        {
            return (default(T), FixedOutcomes.Fail(message, exception));
        }

        public static (T schema, Failure failure) Success(T result)
        {
            return (result, null);
        }
    }
}
