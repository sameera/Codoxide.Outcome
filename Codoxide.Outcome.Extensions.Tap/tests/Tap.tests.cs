using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;

    public class TapExtensionTests
    {
        readonly Action paramlessAction = A.Fake<Action>();
        readonly Action<string> withparamAction = A.Fake<Action<string>>();


        [Fact]
        public void Executes_if_the_precedent_is_successful()
        {
            var successful = Outcome.Of("success");

            successful.Tap(paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustHaveHappenedOnceExactly();

            successful.Tap(withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Doesnt_execute_if_the_precedent_had_failed()
        { 
            var failed = Outcome<string>.Reject("Failed");

            failed.Tap(paramlessAction);
            A.CallTo(() => paramlessAction.Invoke()).MustNotHaveHappened();

            failed.Tap(withparamAction);
            A.CallTo(() => withparamAction.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }
    }
}
