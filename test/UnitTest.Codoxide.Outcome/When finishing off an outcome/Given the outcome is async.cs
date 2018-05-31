using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.When_finishing_off_an_outcome
{
    public class Given_the_outcome_is_async: TestSpec
    {
        [Fact]
        public async Task It_returns_in_the_output_format_of_the_Returns_handler()
        {
            async Task<Outcome<string>> asyncIncrement(int initialValue) {
                await Task.Delay(1);
                return (initialValue + 1).ToString();
            };

            var finalOutcome = await Begin().Then(i => asyncIncrement(i));

            var result = finalOutcome.Return(s => Double.Parse(s)).Unwrap();

            result.Should().BeOfType(typeof(double));
            result.Should().NotBe(-1);
            result.Should().Be(_initialOutcome + 1d);
        }

        [Fact]
        public async Task It_executes_the_catch_handler_of_the_exception_thrown()
        {
            var finalOutcome = await Begin()
                .ThenTry(i => {
                    throw new InvalidOperationException();
                });

            var result = finalOutcome
                .Return(i => i * 10d)
                .Catch<ArgumentException>(fail => -10d)
                .Catch<InvalidOperationException>(fail => -1d)
                .Unwrap();

            result.Should().BeOfType(typeof(double));
            result.Should().NotBe(_initialOutcome * 10d);
            result.Should().NotBe(-10d);
            result.Should().Be(-1);
        }

        private async Task<Outcome<int>> Begin()
        {
            await Task.Delay(1);
            return new Outcome<int>(_initialOutcome);
        }

        private const int _initialOutcome = 100;
    }
}
