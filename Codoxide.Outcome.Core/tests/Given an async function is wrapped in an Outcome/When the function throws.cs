using System;
using System.Threading.Tasks;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_an_async_function_is_wrapped_in_an_Outcome
{
    public class When_the_function_throws
    {
        [Fact]
        public async Task It_returns_a_failure_with_exact_error()
        {
            var outcome = await Outcome.Of(ThrowException);
            outcome.IsSuccessful.Should().BeFalse();
            var ex = outcome.FailureOrThrow().AsException();
            ex.Should().BeOfType<ApplicationException>();
            ex.Message.Should().Be(_expectedException.Message);
        }

        private readonly Exception _expectedException = new ApplicationException("Expected Exception");
        private async Task<int> ThrowException()
        {
            await Task.Delay(0);
            throw _expectedException;
        }

        private async Task<int[]> ThrowAggregateException()
        {
            return await Task.WhenAll(new[] {
                this.ThrowException(),
                this.ThrowException(),
                this.ThrowException()
            });
        }
    }
}