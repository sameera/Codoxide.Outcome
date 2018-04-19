using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Codoxide.Outcome
{
    public class Outcome_of_async_operations
    {
        [Fact]
        public async Task Outcome_of_an_async_method_will_be_unwrapped_by_the_chain()
        {
            async Task<Outcome<string>> asyncIncrement(int initialValue) {
                await Task.Delay(1);
                return (initialValue + 1).ToString();
            };

            var finalOutcome = await Begin()
                .Then(result => {
                    return asyncIncrement(result);
                });

            finalOutcome.Should().NotBeNull();
            finalOutcome.Should().BeOfType<Outcome<string>>();
        }

        [Fact]
        public async Task ValueOutcome_of_an_async_method_will_be_unwrapped_by_the_chain()
        {
            async Task<(string result, Failure failure)> asyncIncrement(int initialValue) {
                await Task.Delay(1);
                return ((initialValue + 1).ToString(), null);
            }

            var finalOutcome = await BeginValueType()
                .Then(result => asyncIncrement(result));

            finalOutcome.result.Should().NotBeNullOrWhiteSpace();
            finalOutcome.result.Should().Be("101");
            finalOutcome.failure.Should().BeNull();
        }

        private Outcome<int> Begin()
        {
            return new Outcome<int>(100);
        }

        private (int result, Failure Failure) BeginValueType()
        {
            return (100, null);
        }
    }
}
