#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide.Internals
{
    using static Codoxide.FixedOutcomes;

    public static class Utility
    {
        public static async Task<Outcome<T>> Try<T>(Func<Task<Outcome<T>>> func)
        {
            try
            {
                return await func().ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return Outcome<T>.Reject(Fail(ex));
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static Outcome<T> Try<T>(Func<Outcome<T>> func)
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
    }
}
