using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.When_finishing_off_an_outcome
{
    public class Given_an_onSuccess_handler_is_provided: TestSpec
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

        [Fact]
        public async Task Value_returned_by_onSuccess_function_is_the_return_value_of_the_chain()
        {
            async Task<ValueTuple<string, Failure>> asyncIncrement(int initialValue)
            {
                await Task.Delay(1);
                return ((initialValue + 1).ToString(), null);
            };

            var finalOutcome = await BeginValueType()
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

        private (int result, Failure Failure) BeginValueType()
        {
            return (_initialOutcome, null);
        }

        private const int _initialOutcome = 100;
    }
}
