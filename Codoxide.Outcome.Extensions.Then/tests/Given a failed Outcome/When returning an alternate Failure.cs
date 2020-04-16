using System;
using Xunit;
using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;

namespace _.Given_a_failed_Outcome
{
    public class When_returning_an_alternate_Failure: GivenFailedOutcome
    {
        [Fact]
        public void It_adopts_the_returned_failure_as_the_ultimate_failure()
        {
            var finalOutcome = TheFailedOutcome
                                .Catch(failure => new Failure("Alternate failure"));

            finalOutcome.IsSuccessful.Should().BeFalse();
            finalOutcome.FailureOrThrow().Reason.Should().Be("Alternate failure");
        }
    }
}
