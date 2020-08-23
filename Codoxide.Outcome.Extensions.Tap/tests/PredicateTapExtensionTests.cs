using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;

    public class PredicateTapExtensionTests
    {
        Action paramlessAction = A.Fake<Action>();
        Action<string> withparamAction = A.Fake<Action<string>>();

        Func<bool> paramlessPredicate = () => true;
        Func<string, bool> withParamPredicate = s => true;


        [Fact]
        public void Executes_if_the_precedent_is_successful_and_predicate_is_true()
        {
            var successful = Outcome.Of("success");

            successful.Tap(paramlessPredicate, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustHaveHappenedOnceExactly();

            successful.Tap(withParamPredicate, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_had_failed_even_if_predicate_is_true()
        { 
            var failed = Outcome<string>.Reject("Failed");

            failed.Tap(paramlessPredicate, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.Tap(paramlessPredicate, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_is_successful_and_predicate_is_false()
        {
            var successful = Outcome.Of("success");

            successful.Tap(false, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            successful.Tap(paramlessPredicate, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Does_not_execute_if_the_precedent_had_failed_even_if_predicate_is_false()
        {
            var failed = Outcome<string>.Reject("Failed");

            failed.Tap(paramlessPredicate, paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.Tap(paramlessPredicate, withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }
    }
}
