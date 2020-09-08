using Codoxide;
using Codoxide.OutcomeExtensions.Filters;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _
{
    public class OtherwiseTests
    {
        [Fact]
        public void It_executes_code_only_if_the_When_condition_was_not_met()
        {
            var mapper = A.Fake<Func<string, string>>();
            var tapper = A.Fake<Action<string>>();

            var sideEffect = "";

            A.CallTo(() => mapper.Invoke(A<string>.Ignored)).Returns("Invoked!");
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).Invokes((string s) => sideEffect = s);

            var original = Outcome.Of("Original");
            var whenSeq = original
                            .When(1 == 2)
                            .Map(mapper)
                            .Tap(tapper);
            var otherSeq = whenSeq
                            .Otherwise()
                            .Map(s => s + "+Otherwise")
                            .Tap(tapper);

            whenSeq.IsSuccessful.Should().BeFalse();
            otherSeq.IsSuccessful.Should().BeTrue();

            A.CallTo(() => mapper.Invoke(A<string>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();

            sideEffect.Should().Be("Original+Otherwise");
        }

        [Fact]
        public void It_can_be_applied_with_many_layers_of_When()
        {
            var mapper = A.Fake<Func<string, string>>();
            var tapper = A.Fake<Action<string>>();

            var sideEffect = "";

            A.CallTo(() => mapper.Invoke(A<string>.Ignored)).Returns("Invoked!");
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).Invokes((string s) => sideEffect = s);

            var original = Outcome.Of("Original");
            var whenSeq = original
                            .When(1 == 2)
                                .Map(mapper)
                                .Tap(tapper)
                                .Catch(f => "Failed 1==2")
                            .When(2 == 3)
                                .Map(mapper)
                                .Tap(tapper)
                                .Catch(f => "Failed 2==3")
                            .When(3 == 4)
                                .Map(mapper)
                                .Tap(tapper)
                                .Catch(f => "Failed 3==4")
                            .Otherwise()
                                .Map(s => s + "+Otherwise")
                                .Tap(tapper)
                                .Catch(f => "Failed Otherwise");

            whenSeq.IsSuccessful.Should().BeTrue();

            A.CallTo(() => mapper.Invoke(A<string>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();

            sideEffect.Should().Be("Original+Otherwise");
        }
    }
}
