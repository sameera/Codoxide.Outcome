using Codoxide;
using Codoxide.Outcomes;
using Xunit;

namespace UnitTest.Codoxide.Outcome
{
    using FluentAssertions;
    using static GenericOutcomes<int>;

    public class Wrapping_results_with_GenericOutcomes
    {
        private const int _fixed_initial_value = 100;

        private (int result, Failure failure) TheFunctionThatCanFail(bool fail)
        {
            if (!fail)
            {
                return Success(_fixed_initial_value);
            }
            else
            {
                return Error("The random failure");
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
