using Codoxide.Outcomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codoxide.Internals
{
    public static class InternalAccessExtensions
    {
        public static T GetResultUnchecked<T>(this Outcome<T> @this) => @this.Result;
    }
}
