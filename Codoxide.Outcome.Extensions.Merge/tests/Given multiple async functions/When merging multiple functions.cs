using System;
using System.Threading.Tasks;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_multiple_sync_functions
{
    public class When_merging_multiple_async_functions: GivenMultipleFunctions
    {
        [Fact]
        public async Task It_can_merge_three_outcomes()
        {
            var value = await GetWrappedAAsync()
                .Merge(GetTodayAsync)
                .Merge((theA, theToday) => {
                    theA.Should().Be(GetA());
                    theToday.Should().Be(GetToday());
                    return Get99Point5Async();
                });

            value.ResultOrThrow().Should().BeOfType<ValueTuple<string, DateTime, double>>();

            var (a, today, dblVal) = value.ResultOrThrow();
            a.Should().Be(GetA());
            today.Should().Be(GetToday());
            dblVal.Should().Be(99.5);
        }       
        
        [Fact]
        public async Task It_can_merge_four_outcomes()
        {
            var value = await GetWrappedAAsync()
                .Merge(GetTodayAsync)
                .Merge((theA, theToday) => Get99Point5Async())
                .Merge((theA, theToday, nnpf) => {
                    theA.Should().Be(GetA());
                    theToday.Should().Be(GetToday());
                    nnpf.Should().Be(99.5);
                    return GetWrappedAAsync();
                });

            value.ResultOrThrow()
                .Should()
                .BeOfType<ValueTuple<string, DateTime, double, string>>();

            var (a, today, nnpf, aAgain) = value.ResultOrThrow();
            a.Should().Be(GetA());
            today.Should().Be(GetToday());
            nnpf.Should().Be(99.5);
            aAgain.Should().Be(GetA());
        }      
              
        
        [Fact]
        public async Task It_can_merge_five_outcomes()
        {
            var value = await GetWrappedAAsync()
                .Merge(GetTodayAsync)
                .Merge((theA, theToday) => Get99Point5Async())
                .Merge((theA, theToday, theNnpf) => GetTypeCodeAsync())
                .Merge((theA, theToday, theNnpf, theTypeCode) => {
                    theA.Should().Be(GetA());
                    theToday.Should().Be(GetToday());
                    theNnpf.Should().Be(99.5);
                    return GetWrappedAAsync();
                });

            value.ResultOrThrow()
                .Should()
                .BeOfType<ValueTuple<string, DateTime, double, TypeCode, string>>();

            var (a, today, nnpf, typeCode, aAgain) = value.ResultOrThrow();
            a.Should().Be(GetA());
            today.Should().Be(GetToday());
            nnpf.Should().Be(99.5);
            typeCode.Should().Be(TypeCode.Boolean);
            aAgain.Should().Be(GetA());
        }         
    }
}