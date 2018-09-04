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
                .Then(result => {
                    return asyncIncrement(result);
                });

            finalOutcome.Should().NotBeNull();
            finalOutcome.Should().BeOfType<Outcome<string>>();
        }

        //[Fact]
        //public async Task ValueOutcome_of_an_async_method_will_be_unwrapped_by_the_chain()
        //{
        //    async Task<(string result, Failure failure)> asyncIncrement(int initialValue) {
        //        await Task.Delay(2000);
        //        return ((initialValue + 1).ToString(), null);
        //    }

        //    var finalOutcome = await BeginValueType()
        //        .Then(result => asyncIncrement(result));

        //    finalOutcome.result.Should().NotBeNullOrWhiteSpace();
        //    finalOutcome.result.Should().Be("101");
        //    finalOutcome.failure.Should().BeNull();
        //}

        //[Fact]
        //public async Task ValueOutcome_of_a_chain_of_async_methods_will_be_unwrapped()
        //{
        //    async Task<(int result, Failure failure)> AsyncBegin() {
        //        await Task.Delay(1);
        //        return BeginValueType();
        //    }

        //    var finalOutcome = await AsyncBegin()
        //                        .Then(async result => {
        //                            await Task.Delay(1);
        //                            return result;
        //                        })
        //                        .Then(result => {
        //                            return ++result;
        //                        });

        //    finalOutcome.result.Should().Be(101);
        //    finalOutcome.failure.Should().BeNull();
        //}

        private Outcome<int> Begin()
        {
            return new Outcome<int>(100);
        }

        //private (int result, Failure Failure) BeginValueType()
        //{
        //    return (100, null);
        //}
    }
}
