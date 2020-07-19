using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.Given_methods_that_throw_exceptions
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
                    error.AsException().Should().NotBeNull();
                    error.AsException().Should().BeOfType<InvalidOperationException>();
                    error.AsException().Message.Should().Be("Expected exception");
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
                    error.AsException().Should().NotBeNull();
                    error.AsException().Should().BeOfType<InvalidOperationException>();
                    error.AsException().Message.Should().Be("Expected exception");
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
