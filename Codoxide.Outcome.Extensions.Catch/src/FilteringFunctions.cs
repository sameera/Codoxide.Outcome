using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codoxide.Outcomes
{
    public static class CatchFilters
    {
        public static bool A<T>(IFailure failure)
            => failure is T || failure.AsException() is T;

        public static Func<IFailure, bool> FailureCode(int code)
            => failure => failure.FailureCode == code;
        
    }
}
