using Codoxide;
using System;
using System.Threading.Tasks;
using Xunit;

namespace _.Given_methods_that_throw_exceptions
{
    public class Given_Then_is_used: TestsWithMethodsThatThrow
    {
        [Fact]
        public void Catch_method_is_executed_and_the_exception_is_not_propogated()
        {
            try
            {
                this.Begin()
                    .Then(() => MethodThatThrowsException())
                    .Then(() => Assert.False(true, "Catch should have been invoked and flow should have exited."))
                    .Catch(error => {
                        error.Should().NotBeNull();
                        error.AsException().Should().BeOfType<InvalidOperationException>();
                        return "Handled";
                    })
                    .ResultOrThrow();
            }
            catch (Exception)
            {
                Assert.False(true, "Exception should not have been propogated.");
            }
        }

        [Fact]
        public async Task Catch_method_is_executed_and_the_exception_is_not_propogated_even_if_its_thrown_async()
        {
            try
            {
                await this.Begin()
                    .Then(() => AsyncMethodThatThrowsException())
                    .Then(() => Assert.False(true, "Catch should have been invoked and flow should have exited."))
                    .Catch(error => {
                        error.Should().NotBeNull();
                        error.AsException().Should().BeOfType<InvalidOperationException>();
                        return "Handled";
                    })
                    .ResultOrThrow();
            }
            catch (Exception)
            {
                Assert.False(true, "Exception should not have been propogated.");
            }
        }
    }
}
