using Codoxide;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace _.When_looping_is_required
{
    public class Given_each_element_needs_to_be_processed
    {
        [Fact]
        public void It_can_perform_a_given_operation_on_all_elements()
        {
            var operation = A.Fake<Action<int>>();
            IEnumerable<int> input = new int[] { 1, 2, 3, 4, 5 };
            var finalResult = Outcome.FromResult(input)
                                .ForEach(i => operation(i))
                                .ResultOrThrow()
                                .ToArray();

            finalResult.Length.Should().Be(5);
            finalResult.Should().BeEquivalentTo(input);
            A.CallTo(operation).MustHaveHappened(5, Times.Exactly);
        }


        [Fact]
        public void It_can_apply_a_given_operation_on_all_elements()
        {
            IEnumerable<int> input = new int[] { 1, 2, 3, 4, 5 };
            var finalResult = Outcome.FromResult(input)
                                .MapAll(i => 100d + i)
                                .ResultOrThrow()
                                .ToArray();

            finalResult.Length.Should().Be(5);
            finalResult.Should().BeEquivalentTo(new double[] {
                101, 102, 103, 104, 105
            });
        }
    }
}
