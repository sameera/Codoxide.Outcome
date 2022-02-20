using System;
using FluentAssertions;
using Xunit;

namespace _.UnitTests
{
    using Codoxide;
    using FakeItEasy;
    using System.Threading.Tasks;

    public class When_TapWhen_is_used_to_execute_action
    {

        [Fact]
        public async Task It_executes_if_the_precedent_is_successful_and_condition_is_true()
        {
            var successful = Outcome.FromResult("success");

            await successful.TapWhen(predicate, anAction);
            await successful.TapWhen(predicateWithParams, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustHaveHappenedOnceExactly();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task It_does_not_execute_if_the_precedent_had_failed_even_if_condition_is_true()
        { 
            var failed = Outcome<string>.Reject("Failed");

            await failed.TapWhen(predicate, anAction);
            await failed.TapWhen(predicateWithParams, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task It_does_not_execute_if_the_precedent_is_successful_and_condition_is_false()
        {
            var successful = Outcome.FromResult("success");

            await successful.TapWhen(falsyPredicate, anAction);
            await successful.TapWhen(falsyPredicateWithParams, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task It_does_not_execute_if_the_precedent_had_failed_even_if_condition_is_false()
        {
            var failed = Outcome<string>.Reject("Failed");

            await failed.TapWhen(falsyPredicate, anAction);
            await failed.TapWhen(falsyPredicateWithParams, anActionWithParams);

            A.CallTo(() => anAction.Invoke()).MustNotHaveHappened();
            A.CallTo(() => anActionWithParams.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task It_can_handle_an_exception_thrown()
        {
            var successful = Outcome.FromResult("success").ForAsync();
            
            var result = await successful
                .TapWhen(x => true,
                    x => AsyncActionThatThrows())
                .Map(aFunc);

            result.IsSuccessful.Should().BeFalse();
            A.CallTo(() => aFunc.Invoke()).MustNotHaveHappened();
        }

        [Fact]
        public async Task It_will_ignore_rejections_as_side_effects_should_not_fail_main_flow()
        {
            var successful = Outcome.FromResult("success").ForAsync();
            
            var result = await successful
                .TapWhen(x => true,
                    x => Outcome.Reject("This will be ignored"))
                .Map(aFunc);

            result.IsSuccessful.Should().BeTrue();
            A.CallTo(() => aFunc.Invoke()).MustHaveHappened();
        }

        readonly Func<Task> anAction = A.Fake<Func<Task>>();
        readonly Func<string, Task> anActionWithParams = A.Fake<Func<string, Task>>();

        private readonly Func<Task<int>> aFunc = A.Fake<Func<Task<int>>>();

        readonly Func<bool> predicate = A.Fake<Func<bool>>();
        readonly Func<string, bool> predicateWithParams = A.Fake<Func<string, bool>>();
        readonly Func<bool> falsyPredicate = A.Fake<Func<bool>>();
        readonly Func<string, bool> falsyPredicateWithParams = A.Fake<Func<string, bool>>();

        public When_TapWhen_is_used_to_execute_action()
        {
            A.CallTo(() => predicate.Invoke()).Returns(true);
            A.CallTo(() => predicateWithParams.Invoke(A<string>.Ignored)).Returns(true);
            A.CallTo(() => falsyPredicate.Invoke()).Returns(false);
            A.CallTo(() => falsyPredicateWithParams.Invoke(A<string>.Ignored)).Returns(false);
            A.CallTo(() => aFunc.Invoke()).Returns(Task.FromResult(987));
        }

        private async Task AsyncActionThatThrows()
        {
            await Task.Delay(1);
            throw new InvalidProgramException();
        }
    }
}
