
using Codoxide;
using FluentAssertions;
using Xunit;

namespace _.Given_a_failed_Outcome
{
    public class When_returning_an_alternate_Outcome
    {
        private readonly Outcome<string> _rejected;

        public When_returning_an_alternate_Outcome()
        {
            _rejected = Outcome<string>.Reject("Already rejected");
        }

        [Fact]
        public void It_can_return_an_alterante_successful_outcome()
        {
            var alternate = _rejected.Catch(failure => new Outcome<string>("Alternate"));

            alternate.IsSuccessful.Should().BeTrue();
            alternate.ResultOrThrow().Should().Be("Alternate");
        }

        [Fact]
        public void It_can_return_another_failed_outcome()
        {
            var alternate = _rejected.Catch(failure => Outcome<string>.Reject("Alternate failure"));

            alternate.IsSuccessful.Should().BeFalse();
            alternate.FailureOrThrow().Reason.Should().Be("Alternate failure");
        }
    }
}
