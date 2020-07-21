using Codoxide;
using FluentAssertions;
using System;
using Xunit;

namespace _.When_finishing_off_an_outcome
{
    public class Given_the_outcome_is_sync
    {
        [Fact]
        public void It_invokes_success_handler_when_the_outcome_is_a_success()
        {
            var result = Begin().Return(i => $"Success-{i}").Unwrap();

            result.Should().BeOfType<string>();
            result.Should().Be("Success-100");
        }

        [Fact]
        public void It_invokes_exception_handler_when_a_specific_exception_is_thrown()
        {
            var result = Begin()
                .Tap(i => {
                    throw new InvalidProgramException("expected-exception");
                })
                .Return(i => "Success-" + i.ToString())
                .Catch(e => e.Reason + " occured.")
                .Unwrap();
        }

        private Outcome<int> Begin()
        {
            return new Outcome<int>(_initialOutcome);
        }

        private const int _initialOutcome = 100;
    }
}
