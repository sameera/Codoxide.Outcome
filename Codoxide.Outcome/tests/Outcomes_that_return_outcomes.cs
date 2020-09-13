namespace UnitTest.Codoxide.Outcome
{
    public class Outcomes_that_return_outcomes
    {

        [Fact]
        public void A_Then_that_returns_an_outcome_doesnt_wrap_return_value_in_another_outcome()
        {
            var outcome = this.BeginTest()
                            .Then(value => new Outcome<int>(++value));

            outcome.Should().BeOfType<Outcome<int>>();
            outcome.ResultOrDefault().Should().BeOfType(typeof(int));
            outcome.ResultOrDefault().Should().Be(_initialTestValue + 1);
        }

        private const int _initialTestValue = 100;

        private Outcome<int> BeginTest()
        {
            return new Outcome<int>(_initialTestValue);
        }

        private (int result, Failure failure) BeginValueTest()
        {
            return (_initialTestValue, null);
        }
    }
}
