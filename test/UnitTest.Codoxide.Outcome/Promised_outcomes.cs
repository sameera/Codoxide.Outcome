using Codoxide;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class Promised_outcomes
    {
        [Fact]
        public async Task Promises_are_awaitable()
        {
            var promise = Begin();
            var outcome = await promise;

            outcome.ResultOrDefault().Should().Be("PromisesSample");
        }

        private async Promise<string> Begin()
        {
            await Task.Delay(2000);
            return "PromisesSample";
        }
    }
}
