using Codoxide;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace _.When_continuing
{
    public class Given_a_failure_or_success
    {
        [Fact]
        public void It_continues_on_with_the_result_of_Continue_block()
        {
            this.GetFailingOutcome()
                .Continue(() => 300)
                .Tap(i => i.Should().Be(300))
                .ResultOrThrow();
        }

        [Fact]
        public void It_provides_failure_to_Continue_block_if_needed()
        {
            this.GetFailingOutcome()
                .Continue((_, failure) => {
                    failure.Reason.Should().Be("Failure!");
                    return 0;
                })
                .ResultOrThrow();
        }

        [Fact]
        public async Task It_can_continue_from_async_failure()
        {
            await this.GetFailingOutcomeAsync()
                .TapFailure(failure => failure.Should().NotBeNull())
                .Continue(() => 300)
                .Tap(i => i.Should().Be(300))
                .ResultOrThrow();
        }

        [Fact]
        public async Task It_can_continue_asyncly()
        {
            await this.GetFailingOutcomeAsync()
                .Continue((_, failure) => Task.FromResult(300))
                .Tap(i => i.Should().Be(300))
                .Map(() => this.GetFailingOutcomeAsync())
                .Continue(() => 400)
                .Tap(i => i.Should().Be(400))
                .ResultOrThrow();
        }

        [Fact]
        public async Task It_can_continue_from_Action_type_blocks()
        {
            await this.GetSuccessOutcomeAsync()
                .Tap(() => DoNothing())
                .TapFailure(() => DoNothing())
                .Continue(() => 100)
                .Map(i => i.Should().Be(100))
                .ResultOrThrow();
        }

        [Fact]
        public async Task It_can_continue_from_Action_type_blocks_and_do_async()
        {
            var continuationTask = Task.FromResult(900);

            await this.GetSuccessOutcomeAsync()
                .Tap(() => DoNothing())
                .TapFailure(() => DoNothing())
                .Continue(() => continuationTask)

                .Map(i => i.Should().Be(900))
                .ResultOrThrow();
        }

        [Fact]
        public async Task It_can_continue_when_upgraded_to_async()
        {
            await this.GetSuccessOutcome()
                .Continue(() => GetSuccessOutcomeAsync())
                .Tap(s => s.Should().Be("Success!"))
                .ResultOrThrow();
        }

        public Outcome<string> GetFailingOutcome() => Outcome<string>.Reject("Failure!");
        public Task<Outcome<string>> GetFailingOutcomeAsync() => Task.FromResult(Outcome<string>.Reject("Failure!"));

        public Outcome<string> GetSuccessOutcome() => (Outcome<string>)"Success!";
        public Task<Outcome<string>> GetSuccessOutcomeAsync() => Task.FromResult((Outcome<string>)"Success!");

        private void DoNothing() {}
    }
}
