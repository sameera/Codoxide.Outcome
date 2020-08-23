using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;

    public class ConditionalTapExtensionTests
    {
        Action paramlessAction = A.Fake<Action>();
        Action<string> withparamAction = A.Fake<Action<string>>();


        [Fact]
        public void Executes_if_the_precedent_is_successful_and_condition_is_true()
        {
            var successful = Outcome.Of("success");

            successful.Tap(true, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustHaveHappenedOnceExactly();

            successful.Tap(true, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_had_failed_even_if_condition_is_true()
        { 
            var failed = Outcome<string>.Reject("Failed");

            failed.Tap(true, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.Tap(true, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_is_successful_and_condition_is_false()
        {
            var successful = Outcome.Of("success");

            successful.Tap(false, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            successful.Tap(false, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_had_failed_even_if_condition_is_false()
        {
            var failed = Outcome<string>.Reject("Failed");

            failed.Tap(false, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.Tap(false, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }
    }
}
