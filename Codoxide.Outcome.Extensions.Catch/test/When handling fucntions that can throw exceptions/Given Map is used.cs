﻿using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.When_handling_fucntions_that_can_throw_exceptions
{
    public class Given_Map_is_used: TestsWithMethodsThatThrow
    {
        [Fact]
        public void Catch_method_is_executed_and_the_exception_is_not_propagated()
        {
            try
            {
                this.Begin()
                    .Map(MethodThatThrowsException)
                    .Tap(() => Assert.False(true, "Catch should have been invoked and flow should have exited."))
                    .Catch((error, failed) => {
                        error.Should().NotBeNull();
                        error.AsException().Should().BeOfType<InvalidOperationException>();
                        return failed;
                    });
            }
            catch (Exception)
            {
                Assert.False(true, "Exception should not have been propogated.");
            }
        }

        [Fact]
        public async Task Catch_method_is_executed_and_the_exception_is_not_propagated_even_if_its_thrown_async()
        {
            try
            {
                await this.Begin()
                    .Map(AsyncMethodThatThrowsException)
                    .Tap(() => Assert.False(true, "Catch should have been invoked and flow should have exited."))
                    .Catch((error, failed) => {
                        error.Should().NotBeNull();
                        error.AsException().Should().BeOfType<InvalidOperationException>();
                        return failed;
                    });
            }
            catch (Exception)
            {
                Assert.False(true, "Exception should not have been propagated.");
            }
        }
    }
}
