using System;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;

    public class TapConditionalTests
    {
        readonly Action paramlessAction = A.Fake<Action>();
        readonly Action<string> withparamAction = A.Fake<Action<string>>();


        [Fact]
        public void Executes_if_the_precedent_is_successful_and_condition_is_true()
        {
            var successful = Outcome.Of("success");

            successful.TapWhen(true, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustHaveHappenedOnceExactly();

            successful.TapWhen(true, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_had_failed_even_if_condition_is_true()
        { 
            var failed = Outcome<string>.Reject("Failed");

            failed.TapWhen(true, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.TapWhen(true, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_is_successful_and_condition_is_false()
        {
            var successful = Outcome.Of("success");

            successful.TapWhen(false, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            successful.TapWhen(false, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_had_failed_even_if_condition_is_false()
        {
            var failed = Outcome<string>.Reject("Failed");

            failed.TapWhen(false, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.TapWhen(false, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }
    }
}
