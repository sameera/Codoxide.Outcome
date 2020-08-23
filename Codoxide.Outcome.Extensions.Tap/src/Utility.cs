using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
    using static FixedOutcomes;
    
    static class Utility
    {
        internal static Outcome<T> Try<T>(Func<Outcome<T>> func)
        {
            try
            {
                return func();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Fail(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        internal static async Task<Outcome<T>> Try<T>(Func<Task<Outcome<T>>> func)
        {
            try
            {
                return await func();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Outcome<T>.Reject(Fail(ex));
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
