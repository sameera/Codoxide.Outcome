using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.When_handling_fucntions_that_can_throw_exceptions
{
    public class Given_TheTry_is_used
    {
        [Fact]
        public void It_captures_the_exception_and_use_the_catch_callback()
        {
            Begin()
                .ThenTry(text => MethodThatThrowsException())
                .Then(() => {
                    Assert.False(true, "Exception should have triggered the Catch handler");
                })
                .Catch(error => {
                    error.Exception.Should().NotBeNull();
                    error.Exception.Should().BeOfType<InvalidOperationException>();
                    error.Exception.Message.Should().Be("Expected exception");
                });
        }

        [Fact]
        public async Task It_captures_the_exception_even_if_the_exception_occured_async()
        {
            await Begin()
                .ThenTry(text => AsyncMethodThatThrowsException())
                .Then(() => {
                    Assert.False(true, "Exception should have triggered the Catch handler");
                })
                .Catch(error => {
                    error.Exception.Should().NotBeNull();
                    error.Exception.Should().BeOfType<InvalidOperationException>();
                    error.Exception.Message.Should().Be("Expected exception");
                });
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
