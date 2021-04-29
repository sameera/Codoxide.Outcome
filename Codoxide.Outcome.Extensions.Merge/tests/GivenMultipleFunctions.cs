using System;
using System.Threading.Tasks;
using Codoxide;

namespace _
{
    public class GivenMultipleFunctions
    {
        protected static string GetA() => "A";
        protected static int GetTen() => 10;
        protected static DateTime GetToday() => DateTime.Today;
        protected static TypeCode GetTypeCode() => TypeCode.Boolean;

        protected static Task<DateTime> GetTodayAsync() => Task.FromResult(GetToday());
        protected static Task WorkAsync() => Task.CompletedTask;
        protected static Task<double> Get99Point5Async() => Task.FromResult(99.5);
        
        protected static Task<TypeCode> GetTypeCodeAsync() => Task.FromResult(TypeCode.Boolean);

        protected static Outcome<string> GetWrappedA() => Outcome.Of(GetA);
        protected static Outcome<int> GetWrappedTen() => Outcome.Of(GetTen);
        protected static Outcome<DateTime> GetWrappedToday() => Outcome.Of(GetToday);

        protected static Task<Outcome<string>> GetWrappedAAsync() =>
            Task.FromResult(GetWrappedA());

        protected static Task<Outcome<Nop>> WrappedWorkAsync() => Outcome.Of(WorkAsync);
        protected static Task<Outcome<double>> GetWrapped99Point5Async() => Outcome.Of(Get99Point5Async);
        protected static Task<Outcome<TypeCode>> GetWrappedTypeCode() => Outcome.Of(GetTypeCodeAsync);
    }
}