using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using Xunit;

namespace _.When_handling_fucntions_that_can_throw_exceptions
{
    public class Given_an_Async_chain : TestsWithMethodsThatThrow
    {
        [Fact]
        public async Task It_can_return_a_failure()
        {
            var t = await Outcome.Any()
                .Then(this.AsyncMethodThatThrowsException)
                
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
                .Then(this.AsyncMethodThatThrowsException)
                
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
                .Then(this.AsyncMethodThatThrowsException)
                
                // sig: Outcome<T> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<T>> handler)
                .Catch(failure => this.GetFailedOutcomeAsync());

            t.IsSuccessful.Should().BeFalse();

            var f = t.FailureOrNull();
            f.Should().NotBeNull();
            f.Reason.Should().Be("Test Async Failure");
        }
        
        [Fact]
        public async Task It_can_return_an_async_failure_without_original_failure()
        {
            var t = await Outcome.Any()
                .Then(this.AsyncMethodThatThrowsException)
                
                // sig: Outcome<T> Catch<T>(this Task<Outcome<T>> @this, Func<Failure, Task<Outcome<T>> handler)
                .Catch(this.GetFailedOutcomeAsync);

            t.IsSuccessful.Should().BeFalse();

            var f = t.FailureOrNull();
            f.Should().NotBeNull();
            f.Reason.Should().Be("Test Async Failure");
        }
    }
}