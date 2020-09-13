using Codoxide;
using Codoxide.Outcomes;
using FakeItEasy;
using FluentAssertions;
using System;
using Xunit;

namespace _.When_using_Finally
{
    public class Given_an_Outcome
    {
        [Fact]
        public void It_executes_the_success_handler_if_precedent_is_successful()
        {

            var result = new Outcome<int>(100)
                            .Finally(_successHandler, _failureHandler);

            A.CallTo(() => _successHandler.Invoke(100)).MustHaveHappened();
            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _failureHandler.Invoke(A<Failure>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void It_executes_the_failure_handler_if_precedent_is_not_successful()
        {

            var result = Outcome<int>.Reject(new Failure("Oops!", 3456)).ForAsync()
                            .Finally(_successHandler, _failureHandler);

            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _failureHandler.Invoke(A<Failure>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void It_returns_an_unwrapped_result()
        {
            var result = new Outcome<int>(100)
                            .Finally(_successHandler, _failureHandler);

            result.Should().Be("Success!");


            result = Outcome<int>.Reject(new Failure("Oops!", 3456))
                            .Finally(_successHandler, _failureHandler);
            result.Should().BeOfType<string>();
            result.Should().Be("Failed!");
        }

        private readonly Func<int, string> _successHandler = A.Fake<Func<int, string>>();
        private readonly Func<Failure, string> _failureHandler = A.Fake<Func<Failure, string>>();

        public Given_an_Outcome()
        {
            A.CallTo(() => _successHandler.Invoke(A<int>.Ignored)).Returns("Success!");
            A.CallTo(() => _failureHandler.Invoke(A<Failure>.Ignored)).Returns("Failed!");
        }
    }
}
