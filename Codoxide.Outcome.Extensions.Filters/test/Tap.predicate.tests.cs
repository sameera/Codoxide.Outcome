using System;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;

    public class TapPredicateTests
    {
        [Fact]
        public void Executes_if_the_precedent_is_successful_and_predicate_returns_true()
        {
            var successful = Outcome.Of("success");

            successful.TapWhen(predicate, anAction);
            successful.TapWhen(predicateWithParams, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustHaveHappenedOnceExactly();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Does_not_even_try_the_predicate_if_precendent_had_failed()
        { 
            var failed = Outcome<string>.Reject("Failed");

            failed.TapWhen(predicate, anAction);
            failed.TapWhen(predicate, anActionWithParams);

            A.CallTo(() => predicate.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();

            A.CallTo(() => predicateWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_is_successful_and_predicate_returns_false()
        {
            var successful = Outcome.Of("success");

            A.CallTo(() => predicate.Invoke()).Returns(false);
            A.CallTo(() => predicateWithParams.Invoke(A<string>.Ignored)).Returns(false);

            successful.TapWhen(false, anAction);
            successful.TapWhen(predicate, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        readonly Action anAction = A.Fake<Action>();
        readonly Action<string> anActionWithParams = A.Fake<Action<string>>();

        readonly Func<bool> predicate = A.Fake<Func<bool>>();
        readonly Func<string, bool> predicateWithParams = A.Fake<Func<string, bool>>();

        public TapPredicateTests()
        {
            A.CallTo(() => predicate.Invoke()).Returns(true);
            A.CallTo(() => predicateWithParams.Invoke(A<string>.Ignored)).Returns(true);
        }
    }
}
