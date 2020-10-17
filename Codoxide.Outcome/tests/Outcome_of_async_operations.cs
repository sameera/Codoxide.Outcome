using Codoxide;
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
                .Map(result => asyncIncrement(result));

            finalOutcome.Should().NotBeNull();
            finalOutcome.Should().BeOfType<Outcome<string>>();
        }

        private Outcome<int> Begin()
        {
            return new Outcome<int>(100);
        }
    }
}
