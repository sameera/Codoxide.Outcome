using Codoxide;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _
{
    using static FixedOutcomes;

    public class Chaining_functions_that_can_fail
    {
        [Fact]
        public void A_rejection_can_be_returned_from_within_a_block()
        {
            var intResult = Outcome.Of(() => "Start of test")
                .Map((value, rejector) =>
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return rejector("Was expecting a value");
                    }
                    return 100;
                });
            intResult.IsSuccessful.Should().BeFalse();
            intResult.FailureOrThrow().Reason.Should().Be("Was expecting a value");
        }

        [Fact]
        public async Task A_rejection_can_be_returned_from_an_async_chain()
        {
            var intResult = await Outcome.Of(
                    () => Task.FromResult("Start of test")
                )
                .Map(value => {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return Fail<int>("Was expecting a value");
                    }
                    return 100;
                });
            intResult.IsSuccessful.Should().BeFalse();
            intResult.FailureOrThrow().Reason
                .Should().Be("Was expecting a value");
        }

        [Fact]
        public async Task A_rejection_can_be_returned_from_an_async_block()
        {
            Func<Task<Outcome<int>>> fnThatReturnsAnAsyncOutcome = 
                () => Outcome.Of(Task.FromResult(100));

            var intResult = await Outcome.Of(() => "Start of test")
                .Map(value => {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return FailAsync<int>("Was expecting a value");
                    }
                    return fnThatReturnsAnAsyncOutcome();
                });
            intResult.IsSuccessful.Should().BeFalse();
            intResult.FailureOrThrow().Reason
                .Should().Be("Was expecting a value");
        }
    }
}
