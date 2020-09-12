using Codoxide;
using Codoxide.OutcomeExtensions.Filters;
using FakeItEasy;
using FluentAssertions;
using System;
using Xunit;

namespace _
{
    using static Codoxide.SwitchOperator;

    public class FitlerTests
    {
        [Fact]
        public void Executes_subsequent_code_only_if_original_boolean_Outcome_is_true()
        {
            var truthy = new Outcome<bool>(true);
            var falsy = new Outcome<bool>(false);

            var mapper = A.Fake<Func<bool, string>>();
            var tapper = A.Fake<Action<string>>();

            A.CallTo(() => mapper.Invoke(A<bool>.Ignored)).Returns("Invoked!");

            var truthySeq = truthy
                            .Filter()
                            .Map(mapper)
                            .Tap(tapper);

            truthySeq.IsSuccessful.Should().BeTrue();
            A.CallTo(() => mapper.Invoke(true)).MustHaveHappenedOnceExactly();
            A.CallTo(() => tapper.Invoke("Invoked!")).MustHaveHappenedOnceExactly();

            Fake.ClearRecordedCalls(mapper);
            Fake.ClearRecordedCalls(tapper);

            var falsySeq = falsy
                            .Filter()
                            .Map(mapper)
                            .Tap(tapper);

            falsySeq.IsSuccessful.Should().BeFalse();
            falsySeq.FailureOrNull().Should().BeAssignableTo<ExpectationFailure>();
            A.CallTo(() => mapper.Invoke(A<bool>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Executes_the_sequence_of_the_matching_value_only()
        {
            var mapper = A.Fake<Func<int, string>>();
            var tapper = A.Fake<Action<int>>();

            string sideEffect = "";

            A.CallTo(() => mapper.Invoke(A<int>.Ignored)).ReturnsLazily((int i) => "Invoked with " + i.ToString());
            A.CallTo(() => tapper.Invoke(A<int>.Ignored)).Invokes((int i) => sideEffect += i.ToString());

            var origin = new Outcome<int>(10);
            var result = Switch(
                            origin.Filter(5)
                                .Tap(tapper)
                                .Map(mapper),
                            origin.Filter(10)
                                .Tap(tapper)
                                .Map(mapper),
                            origin.Filter(15)
                                .Tap(tapper)
                                .Map(mapper)
                        );

            result.IsSuccessful.Should().BeTrue();
            result.ResultOrDefault().Should().Be("Invoked with 10");
            sideEffect.Should().Be("10");

            A.CallTo(() => mapper.Invoke(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Invoke(10)).MustHaveHappened();
            A.CallTo(() => tapper.Invoke(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => tapper.Invoke(10)).MustHaveHappened();
        }
    }
}
