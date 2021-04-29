using System;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_multiple_sync_functions
{
    public class When_merging_multiple_sync_functions: GivenMultipleFunctions
    {
        [Fact]
        public void It_can_merge_three_outcomes()
        {
            var value = GetWrappedA()
                .Merge(GetToday)
                .Merge((theA, theToday) => {
                    theA.Should().Be(GetA());
                    theToday.Should().Be(GetToday());
                    return GetTen();
                });

            value.ResultOrThrow().Should().BeOfType<ValueTuple<string, DateTime, int>>();

            var (a, today, ten) = value.ResultOrThrow();
            a.Should().Be(GetA());
            today.Should().Be(GetToday());
            ten.Should().Be((GetTen()));
        }       
        
        [Fact]
        public void It_can_merge_four_outcomes()
        {
            var value = GetWrappedA()
                .Merge(GetToday)
                .Merge((theA, theToday) => GetTen())
                .Merge((theA, theToday, theTen) => {
                    theA.Should().Be(GetA());
                    theToday.Should().Be(GetToday());
                    return GetWrappedTen();
                });

            value.ResultOrThrow().Should().BeOfType<ValueTuple<string, DateTime, int, int>>();

            var (a, today, ten, wrappedTen) = value.ResultOrThrow();
            a.Should().Be(GetA());
            today.Should().Be(GetToday());
            ten.Should().Be(10);
            wrappedTen.Should().Be(10);
        }      
              
        
        [Fact]
        public void It_can_merge_five_outcomes()
        {
            var value = GetWrappedA()
                .Merge(GetToday)
                .Merge((theA, theToday) => GetTen())
                .Merge((theA, theToday, theTen) => GetTypeCode())
                .Merge((theA, theToday, theTen, theTypeCode) => {
                    theA.Should().Be(GetA());
                    theToday.Should().Be(GetToday());
                    theTen.Should().Be(10);
                    theTypeCode.Should().Be(TypeCode.Boolean);
                    return GetWrappedTen();
                });

            value.ResultOrThrow().Should().BeOfType<ValueTuple<string, DateTime, int, TypeCode, int>>();

            var (a, today, ten, typeCode, wrappedTen) = value.ResultOrThrow();
            a.Should().Be(GetA());
            today.Should().Be(GetToday());
            ten.Should().Be(10);
            typeCode.Should().Be(TypeCode.Boolean);
            wrappedTen.Should().Be(10);
        }      
    }
}