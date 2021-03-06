using Codoxide;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace _.When_processing_async_methods
{
    public class Given_explicit_await_statements_are_not_used: BaseWhen
    {
        [Fact]
        public async void It_still_waits_for_the_statements_to_end()
        {
            bool awaited = false;

            await this.DoAsyncParameterizedOutcome(100d)
                .Map(number => {
                    return this.DoAsyncParameterizedOutcome(number);
                })
                .Map(result => {
                    result.Should().BeOfType(typeof(double));
                    return this.DoAsyncOutcome();
                })
                .Tap(result => {
                    result.Should().Be(_theResult);
                    awaited = true;
                });

            awaited.Should().BeTrue();
        }

        private async Task<Outcome<double>> DoAsyncParameterizedOutcome(double number)
        {
            await Task.Delay(1000);
            return number;
        }
    }
}
