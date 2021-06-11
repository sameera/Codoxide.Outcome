using Codoxide;
using Codoxide.Outcomes;
using Xunit;

namespace UnitTest.Codoxide.OutcomeTests
{
    using FluentAssertions;

    public class Wrapping_results_with_GenericOutcomes
    {
        private const int _fixed_initial_value = 100;

        private (int result, Failure failure) TheFunctionThatCanFail(bool fail)
        {
            if (!fail)
            {
                return (_fixed_initial_value, null);
            }
            else
            {
                return (0, FixedOutcomes.Fail("The random failure"));
            }
        }

        [Fact]
        public void ValueOutcomes_can_be_generated_with_GenericOutcomes()
        {
            (int result, Failure failure) = TheFunctionThatCanFail(false);
            result.Should().Be(_fixed_initial_value);
            failure.Should().BeNull();

            (result, failure) = TheFunctionThatCanFail(true);
            result.Should().Be(default(int));
            failure.Should().NotBeNull();
            failure.Reason.Should().Be("The random failure");
        }
    }
}
