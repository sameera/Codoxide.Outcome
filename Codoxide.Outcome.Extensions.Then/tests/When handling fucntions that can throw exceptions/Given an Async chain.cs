using System.Threading.Tasks;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_methods_that_throw_exceptions
{
    public class Given_an_Async_chain : TestsWithMethodsThatThrow
    {
        [Fact]
        public async Task It_can_return_a_failure()
        {
            var t = await Outcome.Any()
                .Then(AsyncMethodThatThrowsException)
                
                // sig: Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Outcome<T>> handler)
                .Catch(failure => Outcome<string>.Reject("Test Failure", failure.AsException()));

            t.IsSuccessful.Should().BeFalse();

            var f = t.FailureOrNull();
            f.Should().NotBeNull();
            f.Reason.Should().Be("Test Failure");
        }
        
        [Fact]
        public async Task It_can_return_a_failure_without_original_failure()
        {
            var t = await Outcome.Any()
                .Then(AsyncMethodThatThrowsException)
                
                // sig: Task<Outcome<T>> Catch<T>(this Task<Outcome<T>> @this, Func<Outcome<T>> handler)
                .Catch(() => {
                    return Outcome<string>.Reject("Test Failure");
                });

            t.IsSuccessful.Should().BeFalse();

            var f = t.FailureOrNull();
            f.Should().NotBeNull();
            f.Reason.Should().Be("Test Failure");
        }
        
        [Fact]
        public async Task It_can_return_an_async_failure()
        {
            var t = await Outcome.Any()
                .Then(AsyncMethodThatThrowsException)
                
                // sig: Outcome<T> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<T>> handler)
                .Catch(failure => GetFailedOutcomeAsync());

            t.IsSuccessful.Should().BeFalse();

            var f = t.FailureOrNull();
            f.Should().NotBeNull();
            f.Reason.Should().Be("Test Async Failure");
        }
        
        [Fact]
        public async Task It_can_return_an_async_failure_without_original_failure()
        {
            var t = await Outcome.Any()
                .Then(AsyncMethodThatThrowsException)
                
                // sig: Outcome<T> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<T>> handler)
                .Catch(GetFailedOutcomeAsync);

            t.IsSuccessful.Should().BeFalse();

            var f = t.FailureOrNull();
            f.Should().NotBeNull();
            f.Reason.Should().Be("Test Async Failure");
        }
    }
}