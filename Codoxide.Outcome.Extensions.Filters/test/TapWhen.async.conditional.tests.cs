using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;
    using System.Threading.Tasks;

    public class TapAsyncConditionalTests
    {

        [Fact]
        public async Task Executes_if_the_precedent_is_successful_and_condition_is_true()
        {
            var successful = Outcome.Of("success");

            await successful.TapWhen(true, anAction);
            await successful.TapWhen(true, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustHaveHappenedOnceExactly();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Does_not_execute_if_the_precedent_had_failed_even_if_condition_is_true()
        { 
            var failed = Outcome<string>.Reject("Failed");

            await failed.TapWhen(true, anAction);
            await failed.TapWhen(true, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Does_not_execute_if_the_precedent_is_successful_and_condition_is_false()
        {
            var successful = Outcome.Of("success");

            await successful.TapWhen(false, anAction);
            await successful.TapWhen(false, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Does_not_execute_if_the_precedent_had_failed_even_if_condition_is_false()
        {
            var failed = Outcome<string>.Reject("Failed");

            await failed.TapWhen(false, anAction);
            await failed.TapWhen(false, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        readonly Func<Task> anAction = A.Fake<Func<Task>>();
        readonly Func<string, Task> anActionWithParams = A.Fake<Func<string, Task>>();

        public TapAsyncConditionalTests()
        {
        }
    }
}
