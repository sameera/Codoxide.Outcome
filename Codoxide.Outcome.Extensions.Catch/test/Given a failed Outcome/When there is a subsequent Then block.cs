using System;
using System.Collections.Generic;
using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace _.Given_a_failed_Outcome
{
    public class When_there_is_a_subsequent_Then_block: GivenFailedOutcome
    {
        private ITestOutputHelper _console;

        public When_there_is_a_subsequent_Then_block(ITestOutputHelper console)
        {
            _console = console;
        }

        [Theory]
        [MemberData(nameof(UsageScenarios))]
        public void It_is_not_executed(string function, Outcome<object> rejected)
        {
            _console.WriteLine($"Testing: {function}");
            rejected.IsSuccessful.Should().BeFalse();
            rejected.FailureOrThrow().Reason.Should().Be(InitialRejectionReason);
        }

        public static IEnumerable<object[]> UsageScenarios()
        {
            static object[] wrap(string name, Outcome<object> o) => new object[] { name, o };

            yield return wrap(
                "Action",
                TheFailedOutcome.Tap(() => Console.WriteLine("Action"))
            );

            yield return wrap(
                "Action<T>",
                TheFailedOutcome.Tap(s => Console.WriteLine(s))
            );

            yield return wrap(
                "Func<R>",
                TheFailedOutcome.Map(() => 10)
            );

            yield return wrap(
                "Func<T, R>",
                TheFailedOutcome.Map(s => 10)
            );

            yield return wrap(
                "Func<Outcome<R>>",
                TheFailedOutcome.Map(() => new Outcome<int>(10))
            );

            yield return wrap(
                "Func<T, Outcome<R>>",
                TheFailedOutcome.Map(s => new Outcome<int>(10))
            );

            yield return wrap(
                "Func<ValueType<R, Failure>>",
                TheFailedOutcome.Map(() => Tuple.Create<int, Failure>(10, null))
            );

            yield return wrap(
                "Func<T, ValueType<R, Failure>>",
                TheFailedOutcome.Map(s => Tuple.Create(0, new Failure("Dummy failure")))
            );
        }
    }
}