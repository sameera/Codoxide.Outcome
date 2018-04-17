using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Codoxide.Outcome
{
    public class Finishing_up_outcomes
    {

        [Fact]
        public async Task Success_functions_return_type_is_the_return_type_of_the_chain()
        {
            async Task<Outcome<string>> asyncIncrement(int initialValue)
            {
                await Task.Delay(1);
                return (initialValue + 1).ToString();
            };

            var finalOutcome = await Begin()
                .Then(result => {
                    return asyncIncrement(result);
                })
                .Return(
                    success => Double.Parse(success),
                    fail => -1d
                );

            finalOutcome.Should().BeOfType(typeof(double));
            finalOutcome.Should().NotBe(-1);
            finalOutcome.Should().Be(_initialOutcome + 1d);
        }

        private Outcome<int> Begin()
        {
            return new Outcome<int>(_initialOutcome);
        }

        private const int _initialOutcome = 100;
    }
}
