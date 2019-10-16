using System;
using Xunit;
using Codoxide;
using FluentAssertions;
using FakeItEasy;

namespace Codoxide.OutcomeExtensions.Then.UnitTests
{
    public class ThenExtensionsTests
    {
        [Fact]
        public void Executes_the_Then_lambda_function_if_the_outer_Outcome_has_suceeded()
        {
            var successOutcome = Outcome.Any();
            successOutcome.IsSuccessful.Should().BeTrue();

            int i = 0;
            var spy = A.Fake<Func<int>>();
            A.CallTo(() => spy()).ReturnsLazily(() => ++i);

            var operationOutcome = successOutcome.Then(spy);

            A.CallTo(spy).MustHaveHappenedOnceExactly();
            i.Should().Be(1);

            operationOutcome.IsSuccessful.Should().BeTrue();
            operationOutcome.ResultOrDefault().Should().Be(1);
        }

        [Fact]
        public void Then_lambda_is_not_executed_if_the_outer_Outcome_had_failed()
        {
            var failedOutcome = Outcome.Reject("Failed.");
            failedOutcome.IsSuccessful.Should().BeFalse();

            var spy = A.Fake<Func<int>>();
            A.CallTo(() => spy()).Returns(1);

            var outcome = failedOutcome.Then(spy);

            A.CallTo(spy).MustNotHaveHappened();
        }

        [Fact]
        public void Executes_the_Then_lambda_only_after_the_outer_function_has_executed_successfully()
        {
            int i = 0;

            var firstSpy = A.Fake<Action>();
            A.CallTo(() => firstSpy()).Invokes(() => i = 1);

            var secondSpy = A.Fake<Action>();
            A.CallTo(() => secondSpy()).Invokes(() => i *= 10);

            var outcome = Outcome.Any().Then(firstSpy).Then(secondSpy);

            A.CallTo(firstSpy).MustHaveHappenedOnceExactly();
            A.CallTo(secondSpy).MustHaveHappenedOnceExactly();

            i.Should().Be(10); // If it didn't happen in sequence, the value would have been 0
        }


    }
}
