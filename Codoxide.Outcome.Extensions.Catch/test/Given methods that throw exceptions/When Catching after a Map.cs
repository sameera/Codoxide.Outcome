using Codoxide;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace _.Given_methods_that_throw_exceptions
{
    public class When_catching_after_a_Map: GivenMethodsThatThrow
    {
        [Fact]
        public void Catch_method_is_executed_and_the_exception_is_not_propagated()
        {
            try
            {
                var (_, failure) = Begin()
                    .Map(MethodThatThrowsException)
                    .Tap(() => Assert.False(true, "Catch should have been invoked and flow should have exited."))
                    .Catch((error, failed) => {
                        error.Should().NotBeNull();
                        error.ToException().Should().BeOfType<InvalidOperationException>();
                        return failed;
                    });

                failure.Should().NotBeOfType<FalseException>("An Assertion failed.");
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
                var (_, failure) = await Begin()
                    .Map(AsyncMethodThatThrowsException)
                    .Tap(() => Assert.False(true, "Catch should have been invoked and flow should have exited."))
                    .Catch((error, failed) => {
                        error.Should().NotBeNull();
                        error.ToException().Should().BeOfType<InvalidOperationException>();
                        return failed;
                    });

                failure.Should().NotBeOfType<FalseException>("An Assertion failed.");
            }
            catch (Exception)
            {
                Assert.False(true, "Exception should not have been propagated.");
            }
        }
    }
}
