using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using Codoxide;

namespace _.Given_multiple_async_functions
{
    public class When_mixing_with_Map_operations: GivenMultipleFunctions
    {
        [Fact]
        public async Task It_can_map_with_two_functions()
        {
            var value = await GetWrappedAAsync()
                .Merge(GetTen)
                .Map(tuple => {
                    tuple.Item1.Should().Be("A");
                    tuple.Item2.Should().Be(10);
                    return tuple;
                })
                .Merge((a, ten) => {
                    a.Should().Be("A");
                    ten.Should().Be(10);
                    return Task.FromResult(100.5d);
                });

            value.ResultOrThrow().Should().BeOfType<ValueTuple<string, int, double>>();

            var (a, ten, hundredPfive) = value.ResultOrThrow();
            a.Should().Be("A");
            ten.Should().Be(10);
            hundredPfive.Should().Be(100.5);
            
        }
    }
}