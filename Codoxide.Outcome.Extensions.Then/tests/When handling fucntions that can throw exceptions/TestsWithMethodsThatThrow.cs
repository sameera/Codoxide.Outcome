using Codoxide;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _.When_handling_fucntions_that_can_throw_exceptions
{
    public class TestsWithMethodsThatThrow
    {
        protected string MethodThatThrowsException()
        {
            throw new InvalidOperationException("Expected exception");
        }

        protected async Task<string> AsyncMethodThatThrowsException()
        {
            await Task.Delay(1);
            throw new InvalidOperationException("Expected exception");
        }
        protected string MethodThatThrows<T>() where T: Exception, new()
        {
            throw new T();
        }
        protected async Task<string> AsyncMethodThatThrows<T>() where T: Exception, new()
        {
            await Task.Delay(1);
            throw new T();
        }

        protected Outcome<string> Begin()
        {
            return new Outcome<string>("First outcome");
        }
    }
}
