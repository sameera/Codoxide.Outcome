using Codoxide;
using Codoxide.Outcomes;
using FakeItEasy;
using FluentAssertions;
using System;
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

        [Fact]
        public void Convert_Outcome_to_ValueOutcome()
        {
            Outcome<string> outcome = new Outcome<string>("AAB");
            (string result, Failure failure) valueOutcome;
            valueOutcome = outcome;

            valueOutcome.result.Should().Be("AAB");
            valueOutcome.failure.Should().BeNull();

            outcome = FixedOutcomes.Fail("Test Failure");
            valueOutcome = outcome;
            valueOutcome.result.Should().Be(null);
            valueOutcome.failure.Reason.Should().Be("Test Failure");

        }

        [Fact]
        public void Implicitly_casts_Failures_to_Exceptions()
        {
            var logger = A.Fake<Action<Exception, string>>();
            var failure = new Failure("ABCDEF", 101);

            logger(failure, "Logged!");

            A.CallTo(() => logger.Invoke(A<Exception>.That.Matches(e => e.Message == "ABCDEF"), A<string>.Ignored))
                .MustHaveHappened();
        }
    }

}
