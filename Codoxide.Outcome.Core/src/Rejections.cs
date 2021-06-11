using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class Rejections
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
            exception);

        public static Outcome<T> Fail<T>(string reason) => Outcome<T>.Reject(reason);

        public static Task<Outcome<T>> FailAsync<T>(string reason) => 
            Outcome<T>.Reject(reason).ForAsync();
    }

    public static class Rejections<TFailure> where TFailure: Failure
    {
        public static Func<string, Exception, int, TFailure> Factory { get; set; } = 
            (reason, exception, failureCode) => 
                throw new InvalidOperationException(
                        $"A Factory for Rejections<{nameof(TFailure)}> was not configured."
                    );

        public static TFailure Fail(string reason) => 
            Factory(reason, null, Failure.GeneralFailure);
                             
        public static TFailure Fail(string reason, int failureCode) => 
            Factory(reason, null, failureCode);
                             
        public static TFailure Fail(string reason, Exception exception) => 
            Factory(reason, exception, Failure.GeneralFailure);
                             
        public static TFailure Fail(string reason, Exception exception, int failureCode) => 
            Factory(reason, exception, failureCode);
                             
        public static TFailure Fail(Exception exception) => Factory(
            exception?.Message ??
                "Unspecified error. This failure is reported when " +
                "a Failure is returned expecting an expception but " +
                "the given Exception is actually null.",
            exception,
            Failure.GeneralFailure);

        public static Outcome<T> Fail<T>(string reason) => 
            new Outcome<T>(Factory(reason, null, Failure.GeneralFailure));

        public static Task<Outcome<T>> FailAsync<T>(string reason) => 
            Task.FromResult(Fail<T>(reason));
    }
}
