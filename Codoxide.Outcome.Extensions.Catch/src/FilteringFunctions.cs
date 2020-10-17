using System;

namespace Codoxide.Outcomes
{
    public static class CatchFilters
    {
        public static bool A<T>(Failure failure)
            => failure is T || failure.ToException() is T;

        public static Func<Failure, bool> FailureCode(int code)
            => failure => failure.FailureCode == code;
        
    }
}
