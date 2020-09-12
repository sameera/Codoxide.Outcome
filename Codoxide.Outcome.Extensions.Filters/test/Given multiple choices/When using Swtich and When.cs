using Castle.DynamicProxy.Generators;
using Codoxide;
using Codoxide.Outcomes;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _.Given_multiple_choices
{
    public class When_using_Switch_and_When

    {
        [Fact]
        public void Returns_the_outcome_of_the_matched_expectation()
        {
            var mapper = A.Fake<Func<int, string>>();
            var tapper1 = A.Fake<Action<int>>();
            var tapper2 = A.Fake<Action<string>>();
            var catcher = A.Fake<Func<Failure, string>>();

            var mapperCall = A.CallTo(() => mapper.Invoke(A<int>.Ignored));
            mapperCall.ReturnsLazily((int i) => $"Invoked for {i}");

            var catcherCall = A.CallTo(() => catcher.Invoke(A<Failure>.Ignored));
            catcherCall.ReturnsLazily((Failure f) => $"Failed with {f.FailureCode}");

            var matched = new Outcome<int>(100)
                            .Switch(
                                c => c.When(c <= 10, x => x
                                        .Tap(tapper1)
                                        .Map(mapper)
                                        .Catch(catcher)),
                                c => c.When(c <= 100, x => x
                                        .Map(mapper)
                                        .Tap(tapper2)
                                        .Catch(catcher)),
                                c => c.When(c <= 1000, x => x
                                        .Map(mapper)
                                        .Tap(tapper2)
                                        .Catch(catcher))
                            );

            matched.IsSuccessful.Should().BeTrue();
            matched.ResultOrThrow().Should().Be("Invoked for 100");

            mapperCall.MustHaveHappenedOnceExactly();
            catcherCall.MustNotHaveHappened();

            A.CallTo(() => tapper1.Invoke(A<int>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => tapper2.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Invoke(100)).MustHaveHappened();
        }

        [Fact]
        public async Task Returns_the_outcome_of_the_matched_expectation_Async()
        {
            var seq = await new Outcome<string>("Original").ForAsync()
                        .Switch(
                            c => c.When(1 == 2, x => x
                                .Map(s => Task.FromResult(100))
                                .Catch(f => -100)),
                            c => c.When(true, x => x
                                .Map(s => Task.FromResult(50))
                                .Catch(f => -50))
                            );

            seq.IsSuccessful.Should().BeTrue();
            seq.ResultOrDefault().Should().Be(50);
        }

        [Fact]
        public async Task When_clause_can_work_with_complex_types()
        {
            var seq = await Outcome.Of(new WhenClauseTester { TestValue = 100 }).ForAsync()
                        .Switch(
                            c => c.When(c.Result.TestValue == 10, x => x
                                    .Map(s => Task.FromResult(100))
                                    .Catch(f => -100)),
                            c => c.When(c.Result.TestValue == 100, x => x
                                    .Map(s => Task.FromResult(100))
                                    .Catch(f => -100)),
                            c => c.Otherwise(x => x
                                    .Map(s => Task.FromResult(50))
                                    .Catch(f => -50))
                        );

            seq.IsSuccessful.Should().BeTrue();
            seq.ResultOrDefault().Should().Be(100);
        }

        [Fact]
        public void Executes_Otherwise_block_if_no_other_condition_was_met()
        {
            var mapper = A.Fake<Func<string, string>>();
            var tapper = A.Fake<Action<string>>();

            var sideEffect = "";

            A.CallTo(() => mapper.Invoke(A<string>.Ignored)).Returns("Invoked!");
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).Invokes((string s) => sideEffect = s);

            var original = Outcome.Of("Original");
            var whenSeq = original.Switch(
                            c => c.When(1 == 2, x => x
                                    .Map(mapper)
                                    .Tap(tapper)
                                    .Catch(f => "Failed 1==2")),
                            c => c.When(2 == 3, x => x
                                    .Map(mapper)
                                    .Tap(tapper)
                                    .Catch(f => "Failed 2==3")),
                            c => c.When(3 == 4, x => x
                                    .Map(mapper)
                                    .Tap(tapper)
                                    .Catch(f => "Failed 3==4")),
                            c => c.Otherwise(x => x
                                    .Map(s => s + "+Otherwise")
                                    .Tap(tapper)
                                    .Catch(f => "Failed Otherwise"))
                        );

            whenSeq.IsSuccessful.Should().BeTrue();

            A.CallTo(() => mapper.Invoke(A<string>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => tapper.Invoke(A<string>.Ignored)).MustHaveHappenedOnceExactly();

            sideEffect.Should().Be("Original+Otherwise");
        }
    }
    class WhenClauseTester
    {
        public int TestValue { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is int value && value == TestValue) || base.Equals(obj);
        }
    }
}
