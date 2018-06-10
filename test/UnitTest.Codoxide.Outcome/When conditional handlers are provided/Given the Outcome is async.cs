using Codoxide;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace _.When_conditional_handlers_are_provided
{
    public class Given_the_Outcome_is_async
    {
        [Fact]
        public async Task It_only_executes_the_handlers_for_which_the_condition_is_true()
        {
            bool[] hitCounter = new bool[4];

            await Begin()
                .ThenIf(value => value == 100, async value => {
                    // Predicate evaluates to true. Should hit.
                    hitCounter[0] = true;

                    await Task.Delay(1);
                })
                .ThenIf(value => value == 101, async () => {
                    // Predicate evalutes to false. Should not hit.
                    hitCounter[1] = true;

                    await Task.Delay(1);
                })
                .ThenIf(true, async () => {
                    // Condition is true. Should hit.
                    hitCounter[2] = true;

                    await Task.Delay(1);
                })
                .ThenIf(false, async () => {
                    // Condition is false. Should not hit.
                    hitCounter[3] = true;

                    await Task.Delay(1);
                });

            hitCounter.Should().ContainInOrder(new[] {
                true, false, true, false
            });
        }

        private async Task<Outcome<int>> Begin()
        {
            await Task.Delay(1);
            return new Outcome<int>(_initialOutcome);
        }

        private const int _initialOutcome = 100;
    }
}
