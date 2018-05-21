using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace _.When_processing_async_methods
{
    public class Given_an_async_Outcome_is_chained_to_a_ValueOutcome: BaseWhen
    {
        [Fact]
        public async void The_handler_recieves_the_unwrapped_result()
        {
            await this.DoValueOutcome()
                .Then(async number => {
                    return await this.DoAsyncOutcome();
                }).Then(actualReulst => {
                    actualReulst.Should().BeOfType<string>();
                });
        }
    }
}
