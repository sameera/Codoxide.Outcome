using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace _.Given_methods_that_throw_exceptions
{
    public class When_multiple_Catch_blocks_are_present: GivenMethodsThatThrow
    {
        [Fact]
        public void It_only_block_subsequent_Catch_blocks_by_returning_a_provided_Outcome()
        {
            Failure wasReported = null;

            var result = Outcome.Of(MethodThatThrowsException)
                .Catch((failure, ignorable) => {
                    wasReported = failure;
                    return ignorable;
                })
                .Catch(faliure => {
                    Assert.False(true, "Should not have reached this block");
                    return "Worked around";
                })
                .Catch(failure => {
                    Assert.False(true, "Should not have reached this block");
                    return "Worked around";
                });

            result.IsSuccessful.Should().BeFalse();
            result.FailureOrNull().Should().NotBeOfType<FalseException>("An Assertion failed.");
            wasReported.Should().NotBeNull();
        }
    } 
}
