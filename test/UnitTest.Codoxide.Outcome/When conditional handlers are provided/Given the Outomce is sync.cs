using Codoxide;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace _.When_conditional_handlers_are_provided
{
    public class Given_the_Outomce_is_sync
    {
        [Fact]
        public void It_only_executes_the_handlers_for_which_the_condition_is_true()
        {
            bool[] hitCounter = new bool[4];

            Begin()
                .ThenIf(value => value == 100, value => {
                    // Predicate evaluates to true. Should hit.
                    hitCounter[0] = true;
                })
                .ThenIf(value => value == 101, () => {
                    // Predicate evalutes to false. Should not hit.
                    hitCounter[1] = true;
                })
                .ThenIf(true, () => {
                    // Condition is true. Should hit.
                    hitCounter[2] = true;
                })
                .ThenIf(false, () => {
                    // Condition is false. Should not hit.
                    hitCounter[3] = true;
                });

            hitCounter.Should().ContainInOrder(new[] {
                true, false, true, false
            });
        }

        private Outcome<int> Begin()
        {
            return new Outcome<int>(_initialOutcome);
        }

        private const int _initialOutcome = 100;
    }
}
