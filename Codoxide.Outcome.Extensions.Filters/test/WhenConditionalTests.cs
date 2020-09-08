using System.Threading.Tasks;
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _
{
    public class WhenConditionalTests
    {
        [Fact]
        public async Task It_executes_only_the_matching_block()
        {
            var seq = await new Outcome<string>("Original").ForAsync()
                        .When(1 == 2)
                            .Map(s => Task.FromResult(100))
                            .Catch(f => -100)
                        .When(true)
                            .Map(s => Task.FromResult(50))
                            .Catch(f => -50);
            
            seq.IsSuccessful.Should().BeTrue();
            seq.ResultOrDefault().Should().Be(100);
        }
    }
}