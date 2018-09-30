using Codoxide;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _.When_processing_collection_outcomes
{
    public class Given_the_collection_was_produced_successfully
    {
        [Fact]
        public void It_allows_the_collection_to_be_processed_itemwise_and_returns_an_enumerable_outcome_of_results()
        {
            var result = GetCollectionWithoutFailing()
                            .ThenForEach(i => i * 100);

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
