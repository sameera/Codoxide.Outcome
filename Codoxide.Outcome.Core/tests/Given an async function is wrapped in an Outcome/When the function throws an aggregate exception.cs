using System;
using System.Threading.Tasks;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_an_async_function_is_wrapped_in_an_Outcome
{
    public class When_the_function_throws_an_aggregate_exception
    {
        [Fact]
        public async Task It_returns_a_failure_with_the_aggregate_exception()
        {
            var outcome = await Outcome.Of(ThrowAggregateException);
            outcome.IsSuccessful.Should().BeFalse();
            var ex = outcome.FailureOrThrow().AsException();
            ex.Should().BeOfType<AggregateException>();
            
            ((AggregateException)ex).InnerExceptions.Count.Should().Be(3);
        }

        private readonly Exception _expectedException = new ApplicationException("Expected Exception");

        private async Task<int> ThrowException()
        {
            await Task.Delay(0);
            throw _expectedException;
        }

        private Task<int[]> ThrowAggregateException()
        {
            return Task.FromException<int[]>(
                new AggregateException(
                    "One or more errors occured",
                   _expectedException,
                   _expectedException,
                   _expectedException
                ));
        } 
    }
}