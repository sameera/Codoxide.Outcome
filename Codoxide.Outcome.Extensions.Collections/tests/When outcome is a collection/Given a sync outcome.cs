using Codoxide;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _.When_outcome_is_a_collection
{
    public class Given_a_sync_outcome
    {
        [Fact]
        public void It_allows_the_collection_to_be_processed_itemwise_and_returns_an_enumerable_outcome_of_results()
        {
            var result = GetCollectionWithoutFailing().MapAll(i => i * 100);

            result.IsSuccessful.Should().BeTrue();

            var actualResult = result.ResultOrDefault();
            actualResult.Should().NotBeNull();

            actualResult.Count().Should().Be(5);
            actualResult.Should().BeEquivalentTo(new[] { 100, 200, 300, 400, 500 });
        }

        public Outcome<IEnumerable<int>> GetCollectionWithoutFailing()
        {
            return new[] { 1, 2, 3, 4, 5 };
        }
    }
}
