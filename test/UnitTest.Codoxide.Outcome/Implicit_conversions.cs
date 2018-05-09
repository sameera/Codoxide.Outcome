using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using Xunit;

namespace UnitTest.Codoxide.Outcome
{
    public class Implicit_conversions
    {
        [Fact]
        public void Deconstructs_Outcome_to_ValueOutcome()
        {
            Outcome<string> outcome = new Outcome<string>("AAB");
            (string result, Failure failure) = outcome;

            result.Should().Be("AAB");
            failure.Should().BeNull();

            outcome = FixedOutcomes.Fail("Test Failure");
            (result, failure) = outcome;
            result.Should().Be(null);
            failure.Reason.Should().Be("Test Failure");
        }
    }
}
