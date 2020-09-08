using Codoxide;
using Codoxide.Outcomes;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _.When_using_Finally
{
    public class Given_an_async_handlers
    {
        [Fact]
        public async Task It_executes_the_success_handler_if_precedent_is_successful()
        {

            var result = await new Outcome<int>(100).ForAsync()
                            .Finally(_successHandler, _failureHandler);

            A.CallTo(() => _successHandler.Invoke(100)).MustHaveHappened();
            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _failureHandler.Invoke(A<Failure>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task It_executes_the_failure_handler_if_precedent_is_not_successful()
        {

            var result = await Outcome<int>.Reject(new Failure("Oops!", 3456)).ForAsync()
                            .Finally(_successHandler, _failureHandler);

            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).MustNotHaveHappened();

            A.CallTo(
                () => _failureHandler.Invoke(A<Failure>.That.Matches(f => f.Reason == "Oops!" && f.FailureCode == 3456))
            ).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task It_returns_an_unwrapped_result()
        {
            var result = await new Outcome<int>(100).ForAsync()
                            .Finally(_successHandler, _failureHandler);
            result.Should().BeOfType<string>();
            result.Should().Be("Success!");

            result = await Outcome<int>.Reject(new Failure("Oops!", 3456)).ForAsync()
                            .Finally(_successHandler, _failureHandler);
            result.Should().BeOfType<string>();
            result.Should().Be("Failed!");
        }

        private Func<int, Task<string>> _successHandler = A.Fake<Func<int, Task<string>>>();
        private readonly Func<Failure, Task<string>> _failureHandler = A.Fake<Func<Failure, Task<string>>>();

        public Given_an_async_handlers()
        {
            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).Returns(Task.FromResult("Success!"));
            A.CallTo(() => _failureHandler.Invoke(A<Failure>.Ignored)).Returns(Task.FromResult("Failed!"));
        }
    }
}
