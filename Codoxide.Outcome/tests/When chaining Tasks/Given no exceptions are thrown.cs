namespace _.When_chaining_Tasks
{
    public class Given_no_exceptions_are_thrown
    {
        [Fact]
        public async Task It_returns_a_successful_outcome()
        {
            var outcome = await Begin()
                .Then(i => Increment(i))
                .Then(i => Increment(i));

            outcome.IsSuccessful.Should().BeTrue();
            outcome.ResultOrDefault().Should().Be(102);
        }

        private async Task<Outcome<int>> Begin()
        {
            await Task.Yield();
            return 100;
        }

        private async Task<int> Increment(int value)
        {
            await Task.Yield();
            return ++value;
        }

    }
}
