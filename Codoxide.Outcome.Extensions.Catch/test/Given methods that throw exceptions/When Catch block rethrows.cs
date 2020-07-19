using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.Given_methods_that_throw_exceptions
{
    public class When_Catch_block_rethrows: GivenMethodsThatThrow
    {
        [Fact]
        public void It_returns_a_new_failure_with_the_latest_exception()
        {
            var outcome = Outcome.Of(MethodThatThrowsException)
                            .Catch(error => this.MethodThatThrows<FormatException>());

            outcome.IsSuccessful.Should().BeFalse();

            var failure = outcome.FailureOrNull();
            failure.Should().NotBeNull();
            failure.AsException().Should().BeOfType<FormatException>("An Assertion failed.");
        }

        [Fact]
        public async Task It_returns_a_new_failure_with_the_latest_exception_even_when_thrown_async()
        {
            var outcome = await Outcome.Of(AsyncMethodThatThrowsException)
                            .Catch(error => AsyncMethodThatThrows<FormatException>());

            outcome.IsSuccessful.Should().BeFalse();

            var failure = outcome.FailureOrNull();
            failure.Should().NotBeNull();
            failure.AsException().Should().BeOfType<FormatException>("An Assertion failed.");
        }
    }
}
