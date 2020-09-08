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
    public class Given_an_async_Outcome
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

            var expectedParam = A<Failure>.That.Matches(f => f.Reason == "Oops!" && f.FailureCode == 3456);
            A.CallTo(() => _failureHandler.Invoke(expectedParam)).MustHaveHappenedOnceExactly();
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

        private Func<int, string> _successHandler = A.Fake<Func<int, string>>();
        private readonly Func<Failure, string> _failureHandler = A.Fake<Func<Failure, string>>();

        public Given_an_async_Outcome()
        {
            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).Returns("Success!");
            A.CallTo(() => _failureHandler.Invoke(A<Failure>.Ignored)).Returns("Failed!");
        }
    }
}
