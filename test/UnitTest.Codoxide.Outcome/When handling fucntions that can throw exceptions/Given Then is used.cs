using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.When_handling_fucntions_that_can_throw_exceptions
{
    public class Given_Then_is_used
    {
        [Fact]
        public void It_propogates_the_exception_without_invoking_the_catch_handler()
        {
            try
            {
                this.Begin()
                    .Then(() => MethodThatThrowsException())
                    .Then(() => Assert.False(true, "Exception should have been thrown and not silently ignored"))
                    .Catch(error => Assert.False(true, "Exception should have been propagated and not handled in Catch."));
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be("Expected exception");
            }
        }

        [Fact]
        public async Task It_propogates_the_exception_even_if_its_thrown_async()
        {
            try
            {
                await this.Begin()
                    .Then(() => AsyncMethodThatThrowsException())
                    .Then(() => Assert.False(true, "Exception should have been thrown and not silently ignored"))
                    .Catch(error => Assert.False(true, "Exception should have been propagated and not handled in Catch."));
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<InvalidOperationException>();
                ex.Message.Should().Be("Expected exception");
            }
        }

        private string MethodThatThrowsException()
        {
            throw new InvalidOperationException("Expected exception");
        }

        private async Task<string> AsyncMethodThatThrowsException()
        {
            await Task.Delay(1);
            throw new InvalidOperationException("Expected exception");
        }

        private Outcome<string> Begin()
        {
            return new Outcome<string>("First outcome");
        }
    }
}
