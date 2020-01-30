using Codoxide;
using FluentAssertions;
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
                .Then(value => value == 100, value => {
                    // Predicate evaluates to true. Should hit.
                    hitCounter[0] = true;
                })
                .Then(value => value == 101, () => {
                    // Predicate evalutes to false. Should not hit.
                    hitCounter[1] = true;
                })
                .Then(true, () => {
                    // Condition is true. Should hit.
                    hitCounter[2] = true;
                })
                .Then(false, () => {
                    // Condition is false. Should not hit.
                    hitCounter[3] = true;
                });

            hitCounter.Should().ContainInOrder(new[] {
                true, false, true, false
            });
        }

        [Fact]
        public void It_executes_the_handler_or_the_alternate_based_on_condition()
        {
            int[] hitCounter = new int[2] {1, 1};

            Begin()
                .Then(
                    value => value == 100,
                    value => {
                        // Predicate evaluates to true. Should hit.
                        hitCounter[0] = 0;
                    })
                .Then(value => value == 101, 
                    value => {
                        // Predicate evalutes to false. Should not hit.
                        hitCounter[1] = 0;
                    });

            hitCounter.Should().ContainInOrder(new[] { 0, 1 });
        }

        private Outcome<int> Begin()
        {
            return new Outcome<int>(_initialOutcome);
        }

        private const int _initialOutcome = 100;
    }
}
