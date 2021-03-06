using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_an_if_elseif_else_scenario
{
    using static Codoxide.SwitchOperator;

    public class When_using_Switch_operator_and_Filter
    {
        [Fact]
        public void It_returns_the_outcome_of_the_matched_expectation()
        {
            const string TruthyText = "Truthy";
            const string FalseyText = "Falsey";

            var truthy = new Outcome<bool>(true)
                            .Filter()
                            .Map(() => TruthyText);
            var falsey = new Outcome<bool>()
                            .Filter()
                            .Map(() => FalseyText);

            var result = Switch(truthy, falsey);
            
            result.IsSuccessful.Should().BeTrue();
            result.FailureOrNull().Should().BeNull();

            result.ResultOrDefault().Should().Be(TruthyText);

            // Verify that it didn't just return the first outcome

            result = Switch(falsey, truthy);
            result.ResultOrDefault().Should().Be(TruthyText);
        }
    }
}
