using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using Xunit;

namespace _.When_chaining_Outcomes_and_ValueOutcomes
{
    public class Given_they_are_of_different_types: TestSpec
    {
        [Fact]
        public void Result_of_each_step_gets_unwrapped_properly()
        {
            this.FunctionReturningOutcome()
                .Then(r1 => {
                    r1.Should().BeOfType(typeof(int));
                    return FunctionReturningValueOutcome();
                }).Then(r2 => {
                    r2.Should().Be("ValueOutcome");
                });
        }

        [Fact]
        public void Result_unwraps_regardless_of_which_outcome_type_was_used_first()
        {
            this.FunctionReturningValueOutcome()
                .Then(r1 => {
                    r1.Should().BeOfType(typeof(string));
                    return FunctionReturningOutcome();
                }).Then(r2 => {
                    r2.Should().Be(10);
                });
        }

        private (string text, Failure failure) FunctionReturningValueOutcome()
        {
            return ("ValueOutcome", null);
        }

        private Outcome<int> FunctionReturningOutcome()
        {
            return new Outcome<int>(10);
        }
    }
}
