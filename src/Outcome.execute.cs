using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public static class Outcome
    {
        public static async Task<Outcome<T>> Of<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();
                return new Outcome<T>(result);
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
        }

        public static Outcome<T> Of<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return new Outcome<T>(result);
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(e.Message, e);
            }
        }
    }
}
