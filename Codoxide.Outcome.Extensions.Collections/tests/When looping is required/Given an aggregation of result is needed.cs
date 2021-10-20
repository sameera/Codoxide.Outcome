using Codoxide;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _.When_looping_is_required
{
    public class Given_an_aggregation_of_result_is_needed
    {
        [Fact]
        public void It_can_do_so_with_an_aggregaion_function()
        {
            var result = Outcome.FromResult(new int[] { 1, 2, 3, 4, 5 } as IEnumerable<int>)
                            .Reduce((int prv, int cur) => prv + cur)
                            .ResultOrThrow();

            result.Should().Be(15);
        }

        [Fact]
        public async Task It_can_do_so_with_an_async_aggregaion_function()
        {
            var source = Task.FromResult(
                new Outcome<IEnumerable<int>>(
                    new int[] { 1, 2, 3, 4, 5 }
                )
            );

            async Task<Outcome<int>> generatorAsync(int prv, int cur)
            {
                await Task.Delay(0);
                return prv + cur;
            }

            var result = await source
                            .Reduce<int, int>(generatorAsync)
                            .ResultOrThrow();

            result.Should().Be(15);
        }
    }
}
