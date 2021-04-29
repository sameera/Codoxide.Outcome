using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_multiple_async_functions
{
    public class When_merges_two_functions: GivenMultipleFunctions
    {
        [Fact]
        public async Task It_returns_a_two_value_tuple()
        {
            var value = await GetWrapped99Point5Async();
            value.Should().BeOfType<Tuple<string, int>>();
        }
        
        [Fact]
        public void It_returns_a_two_value_tuple_using_first_result_as_param()
        {
            var value = GetWrappedA()
                            .Merge(x => {
                                x.Should().Be(GetA());
                                return GetTen();
                            });

            ValidateTestPassed(value);
        }

        private static void ValidateTestPassed(Outcome<(string, int)> value)
        {
            value.IsSuccessful.Should().BeTrue();
            ValidateTestPassed(value);
        }

        [Fact]
        public void It_merges_outcomes_as_if_they_were_plainer()
        {
            var value = GetWrappedA()
                            .Merge(a => {
                                a.Should().Be(GetA());
                                return GetWrappedTen();
                            });
            ValidateTestPassed(value);
        }

        [Fact]
        public void It_merges_outcomes_as_if_they_were_plainer_using_first_result_as_param()
        {
            var value = GetWrappedA()
                            .Merge(a => {
                                a.Should().Be(GetA());
                                return GetWrappedTen();
                            });
            ValidateTestPassed(value);
        }
    }
}